using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OptiLinq.Collections;

public struct PooledDictionary<TKey, TValue, TComparer> : IDisposable
	where TKey : notnull
	where TComparer : IEqualityComparer<TKey>
{
	private const int Lower31BitMask = 0x7FFFFFFF;

	private readonly ArrayPool<int> _bucketPool;
	private readonly ArrayPool<Slot> _slotPool;
	private readonly TComparer _comparer;

	private int[] _buckets;
	private Slot[] _slots;
	private int _size;

	private int _count;
	private int _lastIndex;

	public int Count => _count;

	public ref TValue this[TKey key]
	{
		get
		{
			ref var value = ref TryGetValue(key);

			if (Unsafe.IsNullRef(ref value))
			{
				throw new KeyNotFoundException();
			}

			return ref value;
		}
	}

	public PooledDictionary(TComparer comparer) : this(4, comparer)
	{
	}

	public PooledDictionary(int capacity, TComparer comparer) : this(capacity, ArrayPool<int>.Shared, ArrayPool<Slot>.Shared, comparer)
	{
	}

	private PooledDictionary(int capacity, ArrayPool<int> bucketPool, ArrayPool<Slot> slotPool, TComparer comparer)
	{
		_bucketPool = bucketPool;
		_slotPool = slotPool;
		_comparer = comparer;

		_size = HashHelpers.GetPrime(capacity);
		_buckets = bucketPool.Rent(_size);
		_slots = slotPool.Rent(_size);

		Array.Clear(_buckets, 0, _buckets.Length);

		_count = 0;
		_lastIndex = 0;
	}

	public void EnsureCapacity(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(capacity));
		}

		if (_size > capacity)
		{
			IncreaseCapacity(capacity);
		}
	}

	public ref TValue TryGetValue(TKey key)
	{
		var hashCode = InternalGetHashCode(key);
		var bucket = hashCode % _size;
		var i = _buckets[bucket] - 1;

		ref var first = ref MemoryMarshal.GetArrayDataReference(_slots);

		while (i >= 0)
		{
			ref var slot = ref Unsafe.Add(ref first, i);

			if (slot.HashCode == hashCode && _comparer.Equals(slot.Key, key))
			{
				return ref slot.Value;
			}

			i = slot.Next;
		}

		return ref Unsafe.NullRef<TValue>();
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(TKey item)
	{
		var hashCode = InternalGetHashCode(item);
		var bucket = hashCode % _size;
		var last = -1;
		var collisionCount = 0;
		var tmpSlots = _slots;

		for (var i = _buckets[bucket] - 1; i >= 0; last = i, i = tmpSlots[i].Next)
		{
			ref var tmpSlot = ref tmpSlots[i];

			if (tmpSlot.HashCode == hashCode && _comparer.Equals(tmpSlot.Key, item))
			{
				if (last < 0)
				{
					_buckets[bucket] = tmpSlot.Next + 1;
				}
				else
				{
					ref var lastSlot = ref tmpSlots[last];
					lastSlot.Next = tmpSlot.Next;
				}

				tmpSlot.HashCode = -1;

				_count--;
				if (_count == 0)
				{
					_lastIndex = 0;
				}

				return true;
			}

			if (collisionCount >= _size)
			{
				throw new InvalidOperationException("Concurrent operations are not supported.");
			}

			collisionCount++;
		}

		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear()
	{
		if (_lastIndex > 0)
		{
			// clear the elements so that the gc can reclaim the references.
			// clear only up to _lastIndex for _slots 
			Array.Clear(_slots, 0, _lastIndex);
			Array.Clear(_buckets, 0, _buckets.Length);
			_lastIndex = 0;
			_count = 0;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Dispose()
	{
		ReturnArrays();
		_size = 0;
		_lastIndex = 0;
		_count = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int InternalGetHashCode(TKey item)
	{
		return _comparer.GetHashCode(item) & Lower31BitMask;
	}

	private void IncreaseCapacity(int newSize)
	{
		newSize = HashHelpers.GetPrime(newSize);

		if (newSize <= _count)
		{
			throw new InvalidOperationException("Capacity overflow");
		}

		// Able to increase capacity; copy elements to larger array and rehash
		SetCapacity(newSize);
	}

	private void SetCapacity(int newSize)
	{
		int[] newBuckets;
		Slot[] newSlots;
		bool replaceArrays;

		// Because ArrayPool might have given us larger arrays than we asked for, see if we can 
		// use the existing capacity without actually resizing.
		if (_buckets?.Length >= newSize && _slots?.Length >= newSize)
		{
			Array.Clear(_buckets, 0, _buckets.Length);
			Array.Clear(_slots, _size, newSize - _size);
			newBuckets = _buckets;
			newSlots = _slots;
			replaceArrays = false;
		}
		else
		{
			newSlots = _slotPool.Rent(newSize);
			newBuckets = _bucketPool.Rent(newSize);

			Array.Clear(newBuckets, 0, newBuckets.Length);
			if (_slots != null)
			{
				Array.Copy(_slots, 0, newSlots, 0, _lastIndex);
			}

			replaceArrays = true;
		}

		for (var i = 0; i < _lastIndex; i++)
		{
			ref var newSlot = ref newSlots[i];
			var bucket = newSlot.HashCode % newSize;
			newSlot.Next = newBuckets[bucket] - 1;
			newBuckets[bucket] = i + 1;
		}

		if (replaceArrays)
		{
			ReturnArrays();
			_slots = newSlots;
			_buckets = newBuckets;
		}

		_size = newSize;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ReturnArrays()
	{
		if (_slots?.Length > 0)
		{
			try
			{
				_slotPool.Return(_slots);
			}
			catch (ArgumentException)
			{
				// oh well, the array pool didn't like our array
			}
		}

		if (_buckets?.Length > 0)
		{
			try
			{
				_bucketPool.Return(_buckets);
			}
			catch (ArgumentException)
			{
				// shucks
			}
		}

		_slots = null;
		_buckets = null;
	}

	public bool Add(TKey key, TValue value) => AddIfNotPresent(key, value, out _);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool AddIfNotPresent(TKey key, TValue value, out int location)
	{
		var hashCode = InternalGetHashCode(key);
		var bucket = hashCode % _size;
		var collisionCount = 0;
		var tmpSlots = _slots;

		ref var first = ref MemoryMarshal.GetArrayDataReference(tmpSlots);

		for (var i = _buckets[bucket] - 1; i >= 0;)
		{
			ref var slot = ref Unsafe.Add(ref first, i);

			if (slot.HashCode == hashCode && _comparer.Equals(slot.Key, key))
			{
				location = i;
				return false;
			}

			if (collisionCount >= _size)
			{
				// The chain of entries forms a loop, which means a concurrent update has happened.
				throw new InvalidOperationException("Concurrent operations are not supported.");
			}

			collisionCount++;
			i = slot.Next;
		}

		if (_lastIndex == _size)
		{
			IncreaseCapacity(_size * 2);
			// this will change during resize
			tmpSlots = _slots;
			bucket = hashCode % _size;
		}

		var index = _lastIndex;
		_lastIndex++;

		ref var slot1 = ref tmpSlots[index];
		slot1.HashCode = hashCode;
		slot1.Key = key;
		slot1.Value = value;
		slot1.Next = _buckets[bucket] - 1;

		_buckets[bucket] = index + 1;
		_count++;

		location = index;
		return true;
	}

	private struct Slot
	{
		internal int HashCode;
		internal int Next;
		internal TKey Key;
		internal TValue Value;
	}
}