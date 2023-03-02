using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OptiLinq.Helpers;

public struct Lookup<TKey, TValue, TComparer> : IDisposable
	where TComparer : IEqualityComparer<TKey>
{
	private const int Lower31BitMask = 0x7FFFFFFF;

	private readonly ArrayPool<int> bucketPool;
	private readonly ArrayPool<Slot<TKey, TValue>> slotPool;
	private readonly TComparer comparer;

	internal int[] buckets;
	internal Slot<TKey, TValue>[] slots;
	private int size;

	private int count;
	private int lastIndex;

	public int Count => count;

	public LookupEnumerator<TKey, TValue> this[TKey key]
	{
		get
		{
			var hashCode = InternalGetHashCode(key);
			var bucket = hashCode % size;
			var collisionCount = 0;
			var tmpSlots = slots;

			for (var i = buckets[bucket] - 1; i >= 0;)
			{
				ref var slot = ref tmpSlots[i];

				if (slot.hashCode == hashCode && comparer.Equals(slot.key, key))
				{
					return new LookupEnumerator<TKey, TValue>(ref slot);
				}

				if (collisionCount >= size)
				{
					// The chain of entries forms a loop, which means a concurrent update has happened.
					throw new InvalidOperationException("Concurrent operations are not supported.");
				}

				collisionCount++;
				i = slot.next;
			}

			throw new KeyNotFoundException("The given key was not present in the lookup.");
		}
	}

	public Lookup(TComparer comparer) : this(4, comparer)
	{
	}

	public Lookup(int capacity, TComparer comparer) : this(capacity, ArrayPool<int>.Shared, ArrayPool<Slot<TKey, TValue>>.Shared, comparer)
	{
	}

	internal Lookup(int capacity, ArrayPool<int> bucketPool, ArrayPool<Slot<TKey, TValue>> slotPool, TComparer comparer)
	{
		this.bucketPool = bucketPool;
		this.slotPool = slotPool;
		this.comparer = comparer;

		size = HashHelpers.GetPrime(capacity);
		buckets = bucketPool.Rent(size);
		Array.Clear(buckets, 0, buckets.Length);
		slots = slotPool.Rent(size);
		count = 0;
		lastIndex = 0;
	}

	public bool TryGetValues(TKey key, out TValue[] values)
	{
		var hashCode = InternalGetHashCode(key);
		var bucket = hashCode % size;
		var collisionCount = 0;
		var tmpSlots = slots;

		for (var i = buckets[bucket] - 1; i >= 0;)
		{
			ref var slot = ref tmpSlots[i];

			if (slot.hashCode == hashCode && comparer.Equals(slot.key, key))
			{
				values = slot.value.ToArray();
				return true;
			}

			if (collisionCount >= size)
			{
				// The chain of entries forms a loop, which means a concurrent update has happened.
				values = Array.Empty<TValue>();
				return false;
			}

			collisionCount++;
			i = slot.next;
		}

		values = Array.Empty<TValue>();
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int InternalGetHashCode(TKey item)
	{
		if (item == null)
		{
			return 0;
		}

		return comparer.GetHashCode(item) & Lower31BitMask;
	}

	private void IncreaseCapacity()
	{
		var newSize = HashHelpers.ExpandPrime(count);
		if (newSize <= count)
		{
			throw new InvalidOperationException("Capacity overflow");
		}

		// Able to increase capacity; copy elements to larger array and rehash
		SetCapacity(newSize);
	}

	private void SetCapacity(int newSize)
	{
		int[] newBuckets;
		Slot<TKey, TValue>[] newSlots;
		bool replaceArrays;

		// Because ArrayPool might have given us larger arrays than we asked for, see if we can 
		// use the existing capacity without actually resizing.
		if (buckets?.Length >= newSize && slots?.Length >= newSize)
		{
			Array.Clear(buckets, 0, buckets.Length);
			Array.Clear(slots, size, newSize - size);
			newBuckets = buckets;
			newSlots = slots;
			replaceArrays = false;
		}
		else
		{
			newSlots = slotPool.Rent(newSize);
			newBuckets = bucketPool.Rent(newSize);

			Array.Clear(newBuckets, 0, newBuckets.Length);
			if (slots != null)
			{
				Array.Copy(slots, 0, newSlots, 0, lastIndex);
			}

			replaceArrays = true;
		}

		for (var i = 0; i < lastIndex; i++)
		{
			ref var newSlot = ref newSlots[i];
			var bucket = newSlot.hashCode % newSize;
			newSlot.next = newBuckets[bucket] - 1;
			newBuckets[bucket] = i + 1;
		}

		if (replaceArrays)
		{
			ReturnArrays();
			slots = newSlots;
			buckets = newBuckets;
		}

		size = newSize;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ReturnArrays()
	{
		if (slots?.Length > 0)
		{
			try
			{
				slotPool.Return(slots);
			}
			catch (ArgumentException)
			{
				// oh well, the array pool didn't like our array
			}
		}

		if (buckets?.Length > 0)
		{
			try
			{
				bucketPool.Return(buckets);
			}
			catch (ArgumentException)
			{
				// shucks
			}
		}

		slots = null;
		buckets = null;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(TKey key, TValue value)
	{
		var hashCode = InternalGetHashCode(key);
		var bucket = hashCode % size;
		var collisionCount = 0;
		var tmpSlots = slots;

		ref var first = ref MemoryMarshal.GetArrayDataReference(tmpSlots);

		for (var i = buckets[bucket] - 1; i >= 0;)
		{
			ref var slot = ref Unsafe.Add(ref first, i);

			if (slot.hashCode == hashCode && comparer.Equals(slot.key, key))
			{
				slot.value.Add(value);
				return;
			}

			if (collisionCount >= size)
			{
				// The chain of entries forms a loop, which means a concurrent update has happened.
				throw new InvalidOperationException("Concurrent operations are not supported.");
			}

			collisionCount++;
			i = slot.next;
		}

		if (lastIndex == size)
		{
			IncreaseCapacity();
			// this will change during resize
			tmpSlots = slots;
			bucket = hashCode % size;
		}

		var index = lastIndex;
		lastIndex++;

		ref var slot1 = ref tmpSlots[index];
		slot1.hashCode = hashCode;
		slot1.key = key;
		slot1.value = new PooledList<TValue>();
		slot1.value.Add(value);

		slot1.next = buckets[bucket] - 1;
		buckets[bucket] = index + 1;
		count++;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(TKey key)
	{
		var hashCode = InternalGetHashCode(key);
		var bucket = hashCode % size;
		var last = -1;
		var collisionCount = 0;
		var tmpSlots = slots;

		for (var i = buckets[bucket] - 1; i >= 0; last = i, i = tmpSlots[i].next)
		{
			ref var tmpSlot = ref tmpSlots[i];
			if (tmpSlot.hashCode == hashCode && comparer.Equals(tmpSlot.key, key))
			{
				if (last < 0)
				{
					buckets[bucket] = tmpSlot.next + 1;
				}
				else
				{
					ref var lastSlot = ref tmpSlots[last];
					lastSlot.next = tmpSlot.next;
				}

				tmpSlot.hashCode = -1;

				count--;
				if (count == 0)
				{
					lastIndex = 0;
				}

				return true;
			}

			if (collisionCount >= size)
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
		if (lastIndex > 0)
		{
			// clear the elements so that the gc can reclaim the references.
			// clear only up to _lastIndex for _slots 
			Array.Clear(slots, 0, lastIndex);
			Array.Clear(buckets, 0, buckets.Length);
			lastIndex = 0;
			count = 0;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Dispose()
	{
		for (var i = 0; i < slots.Length; i++)
		{
			slots[i].value.Dispose();
			slots[i].hashCode = -1;
		}

		ReturnArrays();
		size = 0;
		lastIndex = 0;
		count = 0;
	}
}

internal struct Slot<TKey, TValue>
{
	internal int hashCode;
	internal int next;

	internal TKey key;
	internal PooledList<TValue> value;

	internal Slot(TKey key)
	{
		hashCode = key.GetHashCode();
		this.key = key;
		next = -1;
		value = new PooledList<TValue>(4);
	}
}