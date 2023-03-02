using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OptiLinq.Helpers;

public struct PooledSet<T, TComparer> : IDisposable where TComparer : IEqualityComparer<T>
{
	private const int Lower31BitMask = 0x7FFFFFFF;

	private readonly ArrayPool<int> bucketPool;
	private readonly ArrayPool<Slot<T>> slotPool;
	private readonly TComparer comparer;

	private int[] buckets;
	private Slot<T>[] slots;
	private int size;

	private int count;
	private int lastIndex;

	public int Count => count;

	public PooledSet(TComparer comparer) : this(4, comparer)
	{
	}

	public PooledSet(int capacity, TComparer comparer) : this(capacity, ArrayPool<int>.Shared, ArrayPool<Slot<T>>.Shared, comparer)
	{
	}

	public PooledSet(int capacity, ArrayPool<int> bucketPool, ArrayPool<Slot<T>> slotPool, TComparer comparer)
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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int InternalGetHashCode(T item)
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
		Slot<T>[] newSlots;
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
	public bool AddIfNotPresent(T value)
	{
		var hashCode = InternalGetHashCode(value);
		var bucket = hashCode % size;
		var collisionCount = 0;
		var tmpSlots = slots;

		ref var first = ref MemoryMarshal.GetArrayDataReference(tmpSlots);

		for (var i = buckets[bucket] - 1; i >= 0;)
		{
			ref var slot = ref Unsafe.Add(ref first, i);

			if (slot.hashCode == hashCode && comparer.Equals(slot.value, value))
				return false;

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
		slot1.value = value;
		slot1.next = buckets[bucket] - 1;
		buckets[bucket] = index + 1;
		count++;
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(T item)
	{
		var hashCode = InternalGetHashCode(item);
		var bucket = hashCode % size;
		var last = -1;
		var collisionCount = 0;
		var tmpSlots = slots;
		for (var i = buckets[bucket] - 1; i >= 0; last = i, i = tmpSlots[i].next)
		{
			ref var tmpSlot = ref tmpSlots[i];
			if (tmpSlot.hashCode == hashCode && comparer.Equals(tmpSlot.value, item))
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
		ReturnArrays();
		size = 0;
		lastIndex = 0;
		count = 0;
	}
}

public struct Slot<T>
{
	internal int hashCode;
	internal int next;
	internal T value;
}