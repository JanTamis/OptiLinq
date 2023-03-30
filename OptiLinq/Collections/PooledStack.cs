using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OptiLinq.Collections;

public struct PooledStack<T>
{
	private ArrayPool<T> _pool;
	internal T[] _array; // Storage for stack elements. Do not rename (binary serialization)
	private int _size; // Number of items in the stack. Do not rename (binary serialization)

	private const int DefaultCapacity = 4;

	internal Span<T> AsSpan() => _array.AsSpan(0, _size);

	public PooledStack()
	{
		_array = Array.Empty<T>();
		_pool = ArrayPool<T>.Shared;
	}

	// Create a stack with a specific initial capacity.  The initial capacity
	// must be a non-negative number.
	public PooledStack(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(capacity));
		}

		_pool = ArrayPool<T>.Shared;

		if (capacity == 0)
		{
			_array = Array.Empty<T>();
		}
		else
		{
			_array = _pool.Rent(capacity);
		}
	}

	public int Count
	{
		get => _size;
		internal set => _size = value;
	}

	// Removes all Objects from the Stack.
	public void Clear()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			Array.Clear(_array, 0, _size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references.
		}

		_size = 0;
	}

	public bool Contains(T item)
	{
		// Compare items using the default equality comparer

		// PERF: Internally Array.LastIndexOf calls
		// EqualityComparer<T>.Default.LastIndexOf, which
		// is specialized for different types. This
		// boosts performance since instead of making a
		// virtual method call each iteration of the loop,
		// via EqualityComparer<T>.Default.Equals, we
		// only make one virtual call to EqualityComparer.LastIndexOf.

		return _size != 0 && Array.LastIndexOf(_array, item, _size - 1) != -1;
	}

	// Copies the stack into an array.
	public void CopyTo(Span<T> destination)
	{
		var srcIndex = 0;
		var dstIndex = _size;

		while (srcIndex < _size)
		{
			destination[--dstIndex] = _array[srcIndex++];
		}
	}

	// Returns the top object on the stack without removing it.  If the stack
	// is empty, Peek throws an InvalidOperationException.
	public T Peek()
	{
		var size = _size - 1;
		var array = _array;

		if ((uint)size >= (uint)array.Length)
		{
			ThrowForEmptyStack();
		}

		return array[size];
	}

	public bool TryPeek([MaybeNullWhen(false)] out T result)
	{
		var size = _size - 1;
		var array = _array;

		if ((uint)size >= (uint)array.Length)
		{
			result = default!;
			return false;
		}

		result = array[size];
		return true;
	}

	// Pops an item from the top of the stack.  If the stack is empty, Pop
	// throws an InvalidOperationException.
	public T Pop()
	{
		var size = _size - 1;
		var array = _array;

		// if (_size == 0) is equivalent to if (size == -1), and this case
		// is covered with (uint)size, thus allowing bounds check elimination
		// https://github.com/dotnet/coreclr/pull/9773
		if ((uint)size >= (uint)array.Length)
		{
			ThrowForEmptyStack();
		}

		_size = size;
		var item = array[size];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			array[size] = default!; // Free memory quicker.
		}

		return item;
	}

	public bool TryPop([MaybeNullWhen(false)] out T result)
	{
		var size = _size - 1;
		var array = _array;

		if ((uint)size >= (uint)array.Length)
		{
			result = default!;
			return false;
		}

		_size = size;
		result = array[size];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			array[size] = default!;
		}

		return true;
	}

	// Pushes an item to the top of the stack.
	public void Push(T item)
	{
		var size = _size;
		var array = _array;

		if ((uint)size < (uint)array.Length)
		{
			array[size] = item;
			_size = size + 1;
		}
		else
		{
			PushWithResize(item);
		}
	}

	// Non-inline from Stack.Push to improve its code quality as uncommon path
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void PushWithResize(T item)
	{
		Debug.Assert(_size == _array.Length);
		Grow(_size + 1);
		_array[_size] = item;
		_size++;
	}

	/// <summary>
	/// Ensures that the capacity of this Stack is at least the specified <paramref name="capacity"/>.
	/// If the current capacity of the Stack is less than specified <paramref name="capacity"/>,
	/// the capacity is increased by continuously twice current capacity until it is at least the specified <paramref name="capacity"/>.
	/// </summary>
	/// <param name="capacity">The minimum capacity to ensure.</param>
	/// <returns>The new capacity of this stack.</returns>
	public int EnsureCapacity(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(capacity));
		}

		if (_array.Length < capacity)
		{
			Grow(capacity);
		}

		return _array.Length;
	}

	private void Grow(int capacity)
	{
		Debug.Assert(_array.Length < capacity);

		var newcapacity = _array.Length == 0 ? DefaultCapacity : 2 * _array.Length;

		// Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
		// Note that this check works even when _items.Length overflowed thanks to the (uint) cast.
		if ((uint)newcapacity > Array.MaxLength) newcapacity = Array.MaxLength;

		// If computed capacity is still less than specified, set to the original argument.
		// Capacities exceeding Array.MaxLength will be surfaced as OutOfMemoryException by Array.Resize.
		if (newcapacity < capacity) newcapacity = capacity;

		var newArray = _pool.Rent(newcapacity);
		_array.CopyTo(newArray, 0);

		_pool.Return(_array);
	}

	// Copies the Stack to an array, in the same order Pop would return the items.
	public T[] ToArray()
	{
		if (_size == 0)
			return Array.Empty<T>();

		var objArray = new T[_size];
		var i = 0;
		while (i < _size)
		{
			objArray[i] = _array[_size - i - 1];
			i++;
		}

		return objArray;
	}

	private void ThrowForEmptyStack()
	{
		Debug.Assert(_size == 0);
		throw new InvalidOperationException("Stack is empty.");
	}
}