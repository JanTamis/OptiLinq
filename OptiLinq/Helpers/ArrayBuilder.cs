namespace OptiLinq.Helpers;

internal struct ArrayBuilder<T>
{
	private const int DefaultCapacity = 4;

	private T[]? _array; // Starts out null, initialized on first Add.
	private int _count; // Number of items into _array we're using.

	/// <summary>
	/// Initializes the <see cref="ArrayBuilder{T}"/> with a specified capacity.
	/// </summary>
	/// <param name="capacity">The capacity of the array to allocate.</param>
	public ArrayBuilder(int capacity) : this()
	{
		if (capacity > 0)
		{
			_array = new T[capacity];
		}
	}

	/// <summary>
	/// Gets the number of items this instance can store without re-allocating,
	/// or 0 if the backing array is <c>null</c>.
	/// </summary>
	public int Capacity => _array?.Length ?? 0;

	/// <summary>Gets the current underlying array.</summary>
	public T[]? Buffer => _array;

	/// <summary>
	/// Gets the number of items in the array currently in use.
	/// </summary>
	public int Count => _count;

	/// <summary>
	/// Gets or sets the item at a certain index in the array.
	/// </summary>
	/// <param name="index">The index into the array.</param>
	public T this[int index] => _array![index];

	/// <summary>
	/// Adds an item to the backing array, resizing it if necessary.
	/// </summary>
	/// <param name="item">The item to add.</param>
	public void Add(T item)
	{
		if (_count == Capacity)
		{
			EnsureCapacity(_count + 1);
		}

		UncheckedAdd(item);
	}

	/// <summary>
	/// Gets the first item in this builder.
	/// </summary>
	public T First()
	{
		return _array![0];
	}

	/// <summary>
	/// Gets the last item in this builder.
	/// </summary>
	public T Last()
	{
		return _array![_count - 1];
	}

	/// <summary>
	/// Creates an array from the contents of this builder.
	/// </summary>
	/// <remarks>
	/// Do not call this method twice on the same builder.
	/// </remarks>
	public T[] ToArray()
	{
		if (_count == 0)
		{
			return Array.Empty<T>();
		}

		var result = _array;
		
		if (_count < result.Length)
		{
			// Avoid a bit of overhead (method call, some branches, extra codegen)
			// which would be incurred by using Array.Resize
			result = new T[_count];
			Array.Copy(_array, result, _count);
		}

#if DEBUG
            // Try to prevent callers from using the ArrayBuilder after ToArray, if _count != 0.
            _count = -1;
            _array = null;
#endif

		return result;
	}

	/// <summary>
	/// Adds an item to the backing array, without checking if there is room.
	/// </summary>
	/// <param name="item">The item to add.</param>
	/// <remarks>
	/// Use this method if you know there is enough space in the <see cref="ArrayBuilder{T}"/>
	/// for another item, and you are writing performance-sensitive code.
	/// </remarks>
	public void UncheckedAdd(T item)
	{
		_array![_count++] = item;
	}

	private void EnsureCapacity(int minimum)
	{
		var capacity = Capacity;
		var nextCapacity = capacity == 0 
			? DefaultCapacity 
			: 2 * capacity;

		if ((uint)nextCapacity > (uint)Array.MaxLength)
		{
			nextCapacity = Math.Max(capacity + 1, Array.MaxLength);
		}

		nextCapacity = Math.Max(nextCapacity, minimum);

		var next = new T[nextCapacity];
		if (_count > 0)
		{
			Array.Copy(_array!, next, _count);
		}

		_array = next;
	}
}