using System.Buffers;
using System.Runtime.CompilerServices;

namespace OptiLinq.Helpers;

internal struct PooledList<T> : IDisposable
{
	private readonly ArrayPool<T> pool;
	private int index;
	internal T[] Items;

	public int Count => index + 1;

	public T this[int index]
	{
		get => Items[index];
		set => Items[index] = value;
	}

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
		index = -1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void IncreaseCapacity()
	{
		var destinationArray = pool.Rent(index * 2);
		if (index > 0)
			Array.Copy(Items, destinationArray, index);
		ReturnArray();
		Items = destinationArray;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ReturnArray()
	{
		if (Items is null || Items.Length == 0)
			return;

		pool.Return(Items);
		Items = Array.Empty<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(T item)
	{
		if (++index >= Items.Length)
			IncreaseCapacity();

		Items[index] = item;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(in T item)
	{
		if (++index >= Items.Length)
			IncreaseCapacity();
		Items[index] = item;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Dispose()
	{
		ReturnArray();
		index = -1;
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
}