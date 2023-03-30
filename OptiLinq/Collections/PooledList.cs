using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OptiLinq.Collections;

public struct PooledList<T> : IDisposable
{
	private readonly ArrayPool<T> pool;

	private int _index;
	internal T[] Items;

	public int Capacity
	{
		get => Items.Length;
		set
		{
			if (value != Items.Length)
			{
				if (value > 0)
				{
					IncreaseCapacity(value);
				}
				else
				{
					pool.Return(Items);
					Items = Array.Empty<T>();
					_index = -1;
				}
			}
		}
	}

	public int Count
	{
		get => _index + 1;
		internal set => _index = value - 1;
	}

	public T this[int index]
	{
		get
		{
			if (index >= Count)
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}

			return Items[index];
		}
		set
		{
			if (index >= Count)
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}

			Items[index] = value;
		}
	}

	internal ref T GetRef(int index)
	{
		return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(Items), index);
	}

	public Span<T> AsSpan() => Items.AsSpan(0, Count);

	public PooledList() : this(4)
	{
	}

	public PooledList(int capacity) : this(capacity, ArrayPool<T>.Shared)
	{
	}

	public PooledList(int capacity, ArrayPool<T> pool)
	{
		this.pool = pool;
		Items = capacity > 0 ? pool.Rent(capacity) : Array.Empty<T>();
		_index = -1;
	}

	public PooledList(ReadOnlySpan<T> data) : this(data.Length)
	{
		data.CopyTo(Items);

		_index = data.Length - 1;
	}

	public void EnsureCapacity(int capacity)
	{
		if (Items.Length < capacity)
		{
			IncreaseCapacity(capacity);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(T item)
	{
		if (++_index >= Items.Length)
			IncreaseCapacity(_index * 2);

		Items[_index] = item;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(in T item)
	{
		if (++_index >= Items.Length)
			IncreaseCapacity(_index * 2);
		Items[_index] = item;
	}

	public void AddRange<TEnumerator>(TEnumerator enumerator) where TEnumerator : IEnumerator<T>
	{
		while (enumerator.MoveNext())
		{
			Add(enumerator.Current);
		}

		enumerator.Dispose();
	}

	public int BinarySearch<TComparer>(T item, int index, int length, TComparer comparer) where TComparer : IComparer<T>
	{
		var lo = index;
		var hi = index + length - 1;

		while (lo <= hi)
		{
			var i = lo + ((hi - lo) >> 1);
			int order;

			if (Items[i] is null)
			{
				order = item is null ? 0 : -1;
			}
			else
			{
				order = comparer.Compare(Items[i], item);
			}

			switch (order)
			{
				case 0:
					return i;
				case < 0:
					lo = i + 1;
					break;
				default:
					hi = i - 1;
					break;
			}
		}

		return ~lo;
	}

	public int BinarySearch<TComparer>(T item, TComparer comparer) where TComparer : IComparer<T>
	{
		return BinarySearch(item, 0, Count, comparer);
	}

	public void Clear()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			var size = _index;

			if (size > 0)
			{
				Array.Clear(Items, 0, size); // Clear the elements so that the gc can reclaim the references.
			}
		}

		_index = 0;
	}

	public bool Contains(T item)
	{
		// PERF: IndexOf calls Array.IndexOf, which internally
		// calls EqualityComparer<T>.Default.IndexOf, which
		// is specialized for different types. This
		// boosts performance since instead of making a
		// virtual method call each iteration of the loop,
		// via EqualityComparer<T>.Default.Equals, we
		// only make one virtual call to EqualityComparer.IndexOf.

		return _index != 0 && IndexOf(item) >= 0;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(Items, 0, array, arrayIndex, Count);
	}

	public void CopyTo(int index, T[] array, int arrayIndex, int count)
	{
		Array.Copy(Items, index, array, arrayIndex, count);
	}

	public int CopyTo(Span<T> span)
	{
		var count = Math.Min(span.Length, Count);
		Items.AsSpan(0, count).CopyTo(span.Slice(0, count));

		return count;
	}

	public int EnsureCapacity()
	{
		if (_index == Items.Length - 1)
		{
			IncreaseCapacity(_index * 2);
		}

		return _index + 1;
	}

	public int IndexOf(T item)
	{
		return Array.IndexOf(Items, item, 0, _index);
	}

	public void Insert(int index, T item)
	{
		// Note that insertions at the end are legal.
		if ((uint)index > (uint)_index)
		{
			throw new ArgumentOutOfRangeException(nameof(index));
		}

		if (_index == Items.Length)
			IncreaseCapacity(_index * 2);

		if (index < _index)
		{
			Array.Copy(Items, index, Items, index + 1, _index - index);
		}

		Items[index] = item;
		_index++;
	}

	public void InsertRange(int index, T[] items)
	{
		// Note that insertions at the end are legal.
		if ((uint)index > (uint)_index)
		{
			throw new ArgumentOutOfRangeException(nameof(index));
		}

		var count = items.Length;

		if (_index + count >= Items.Length)
			IncreaseCapacity(_index + count);

		if (index < _index)
		{
			Array.Copy(Items, index, Items, index + count, _index - index);
		}

		Array.Copy(items, 0, Items, index, count);
		_index += count;
	}

	public bool Remove(T item)
	{
		var index = IndexOf(item);

		if (index >= 0)
		{
			RemoveAt(index);
			return true;
		}

		return false;
	}

	public void RemoveAt(int index)
	{
		if ((uint)index >= (uint)_index)
		{
			throw new ArgumentOutOfRangeException(nameof(index));
		}

		_index--;
		if (index < _index)
		{
			Array.Copy(Items, index + 1, Items, index, _index - index);
		}

		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			Items[_index] = default!;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Dispose()
	{
		ReturnArray();
		_index = -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T[] ToArray()
	{
		if (Count == 0)
		{
			return Array.Empty<T>();
		}

		var destinationArray = GC.AllocateUninitializedArray<T>(Count);
		Array.Copy(Items, destinationArray, Count);
		return destinationArray;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void IncreaseCapacity(int size)
	{
		var destinationArray = pool.Rent(size);

		if (_index > 0)
			Array.Copy(Items, destinationArray, _index);

		ReturnArray();
		Items = destinationArray;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ReturnArray()
	{
		if (Items is null or { Length: 0 })
			return;

		pool.Return(Items, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
		Items = Array.Empty<T>();
	}
}