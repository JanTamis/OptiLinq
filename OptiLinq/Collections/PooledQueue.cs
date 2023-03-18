using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OptiLinq.Collections;

// A simple Queue of generic objects.  Internally it is implemented as a
// circular buffer, so Enqueue can be O(n).  Dequeue is O(1).
[DebuggerDisplay("Count = {Count}")]
[Serializable]
public struct PooledQueue<T> : IDisposable
{
	internal T[] _array;
	private int _head; // The index from which to dequeue if the queue isn't empty.
	private int _tail; // The index at which to enqueue if the queue isn't full.
	private int _size; // Number of elements.

	// Creates a queue with room for capacity objects. The default initial
	// capacity and grow factor are used.
	public PooledQueue() : this(4)
	{
		
	}

	// Creates a queue with room for capacity objects. The default grow factor
	// is used.
	public PooledQueue(int capacity)
	{
		_array = ArrayPool<T>.Shared.Rent(capacity);
	}

	public int Count
	{
		get => _size;
		internal set => _size = value;
	}

	// Removes all Objects from the queue.
	public void Clear()
	{
		if (_size != 0)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				if (_head < _tail)
				{
					Array.Clear(_array, _head, _size);
				}
				else
				{
					Array.Clear(_array, _head, _array.Length - _head);
					Array.Clear(_array, 0, _tail);
				}
			}

			_size = 0;
		}

		_head = 0;
		_tail = 0;
	}

	// CopyTo copies a collection into an Array, starting at a particular
	// index into the array.
	public int CopyTo(Span<T> data)
	{
		var numToCopy = _size;

		if (numToCopy == 0)
			return 0;

		var firstPart = Math.Min(_array.Length - _head, Math.Min(numToCopy, data.Length));

		_array.AsSpan(_head, firstPart).CopyTo(data);

		numToCopy -= firstPart;
		if (numToCopy > 0)
		{
			_array.AsSpan(0, numToCopy).CopyTo(data.Slice(firstPart));
		}

		return firstPart;
	}

	// Adds item to the tail of the queue.
	public void Enqueue(T item)
	{
		if (_size == _array.Length)
		{
			Grow(_size + 1);
		}

		_array[_tail] = item;
		MoveNext(ref _tail);
		_size++;
	}

	// Removes the object at the head of the queue and returns it. If the queue
	// is empty, this method throws an
	// InvalidOperationException.
	public T Dequeue()
	{
		var head = _head;
		var array = _array;

		if (_size == 0)
		{
			ThrowForEmptyQueue();
		}

		var removed = array[head];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			array[head] = default!;
		}

		MoveNext(ref _head);
		_size--;
		return removed;
	}

	public bool TryDequeue([MaybeNullWhen(false)] out T result)
	{
		var head = _head;
		var array = _array;

		if (_size == 0)
		{
			result = default;
			return false;
		}

		result = array[head];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			array[head] = default!;
		}

		MoveNext(ref _head);
		_size--;
		return true;
	}

	// Returns the object at the head of the queue. The object remains in the
	// queue. If the queue is empty, this method throws an
	// InvalidOperationException.
	public T Peek()
	{
		if (_size == 0)
		{
			ThrowForEmptyQueue();
		}

		return _array[_head];
	}

	public bool TryPeek([MaybeNullWhen(false)] out T result)
	{
		if (_size == 0)
		{
			result = default;
			return false;
		}

		result = _array[_head];
		return true;
	}

	// Returns true if the queue contains at least one object equal to item.
	// Equality is determined using EqualityComparer<T>.Default.Equals().
	public bool Contains(in T item)
	{
		if (_size == 0)
		{
			return false;
		}

		if (_head < _tail)
		{
			return Array.IndexOf(_array, item, _head, _size) >= 0;
		}

		// We've wrapped around. Check both partitions, the least recently enqueued first.
		return
			Array.IndexOf(_array, item, _head, _array.Length - _head) >= 0 ||
			Array.IndexOf(_array, item, 0, _tail) >= 0;
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		if (_size == 0)
		{
			return false;
		}

		if (_head < _tail)
		{
			for (var i = _head; i < _size + _head; i++)
			{
				if (comparer.Equals(_array[i], item))
				{
					return true;
				}
			}

			return false;
		}

		for (var i = _head; i < _array.Length; i++)
		{
			if (comparer.Equals(_array[i], item))
			{
				return true;
			}
		}

		for (var i = 0; i < _tail; i++)
		{
			if (comparer.Equals(_array[i], item))
			{
				return true;
			}
		}

		return false;
	}

	// Iterates over the objects in the queue, returning an array of the
	// objects in the Queue, or an empty array if the queue is empty.
	// The order of elements in the array is first in to last in, the same
	// order produced by successive calls to Dequeue.
	public T[] ToArray()
	{
		if (_size == 0)
		{
			return Array.Empty<T>();
		}

		var arr = GC.AllocateUninitializedArray<T>(_size);

		if (_head < _tail)
		{
			Array.Copy(_array, _head, arr, 0, _size);
		}
		else
		{
			Array.Copy(_array, _head, arr, 0, _array.Length - _head);
			Array.Copy(_array, 0, arr, _array.Length - _head, _tail);
		}

		return arr;
	}

	// PRIVATE Grows or shrinks the buffer to hold capacity objects. Capacity
	// must be >= _size.
	private void SetCapacity(int capacity)
	{
		var newarray = ArrayPool<T>.Shared.Rent(capacity);

		if (_size > 0)
		{
			if (_head < _tail)
			{
				Array.Copy(_array, _head, newarray, 0, _size);
			}
			else
			{
				Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
				Array.Copy(_array, 0, newarray, _array.Length - _head, _tail);
			}
		}

		ArrayPool<T>.Shared.Return(_array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());

		_array = newarray;
		_head = 0;
		_tail = (_size == capacity) ? 0 : _size;
	}

	// Increments the index wrapping it if necessary.
	private void MoveNext(ref int index)
	{
		// It is tempting to use the remainder operator here but it is actually much slower
		// than a simple comparison and a rarely taken branch.
		// JIT produces better code than with ternary operator ?:
		var tmp = index + 1;
		if (tmp == _array.Length)
		{
			tmp = 0;
		}

		index = tmp;
	}

	private void ThrowForEmptyQueue()
	{
		Debug.Assert(_size == 0);
		throw new InvalidOperationException("Queue is empty.");
	}

	public void TrimExcess()
	{
		var threshold = (int)(_array.Length * 0.9);
		if (_size < threshold)
		{
			SetCapacity(_size);
		}
	}

	/// <summary>
	/// Ensures that the capacity of this Queue is at least the specified <paramref name="capacity"/>.
	/// </summary>
	/// <param name="capacity">The minimum capacity to ensure.</param>
	/// <returns>The new capacity of this queue.</returns>
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

		const int GrowFactor = 2;
		const int MinimumGrow = 4;

		var newcapacity = GrowFactor * _array.Length;

		// Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
		// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
		if ((uint)newcapacity > Array.MaxLength) newcapacity = Array.MaxLength;

		// Ensure minimum growth is respected.
		newcapacity = Math.Max(newcapacity, _array.Length + MinimumGrow);

		// If the computed capacity is still less than specified, set to the original argument.
		// Capacities exceeding Array.MaxLength will be surfaced as OutOfMemoryException by Array.Resize.
		if (newcapacity < capacity) newcapacity = capacity;

		SetCapacity(newcapacity);
	}

	public void Dispose()
	{
		ArrayPool<T>.Shared.Return(_array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
	}
}