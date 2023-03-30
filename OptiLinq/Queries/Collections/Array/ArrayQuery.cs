using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T> : IOptiQuery<T, ArrayEnumerator<T>>
{
	private readonly T[] _array;

	internal ArrayQuery(T[] list)
	{
		_array = list;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		foreach (var item in _array)
		{
			seed = func.Eval(in seed, in item);
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		foreach (var item in _array)
		{
			seed = @operator.Eval(in seed, in item);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		foreach (var item in _array)
		{
			if (!@operator.Eval(in item))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _array.Length != 0;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		foreach (var item in _array)
		{
			if (@operator.Eval(in item))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _array;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer = default) where TComparer : IEqualityComparer<T>
	{
		if (typeof(TComparer) == typeof(EqualityComparer<T>))
		{
			return Contains(in item);
		}
		
		for (var i = 0; i < _array.Length; i++)
		{
			if (comparer.Equals(_array[i], item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			if (Unsafe.SizeOf<T>() == sizeof(byte))
			{
				return SpanHelper.ContainsValueType(
					ref Unsafe.As<T, byte>(ref MemoryMarshal.GetArrayDataReference(_array)),
					Unsafe.As<T, byte>(ref Unsafe.AsRef(in item)),
					_array.Length);
			}

			if (Unsafe.SizeOf<T>() == sizeof(short))
			{
				return SpanHelper.ContainsValueType(
					ref Unsafe.As<T, short>(ref MemoryMarshal.GetArrayDataReference(_array)),
					Unsafe.As<T, short>(ref Unsafe.AsRef(in item)),
					_array.Length);
			}

			if (Unsafe.SizeOf<T>() == sizeof(int))
			{
				return SpanHelper.ContainsValueType(
					ref Unsafe.As<T, int>(ref MemoryMarshal.GetArrayDataReference(_array)),
					Unsafe.As<T, int>(ref Unsafe.AsRef(in item)),
					_array.Length);
			}

			if (Unsafe.SizeOf<T>() == sizeof(long))
			{
				return SpanHelper.ContainsValueType(
					ref Unsafe.As<T, long>(ref MemoryMarshal.GetArrayDataReference(_array)),
					Unsafe.As<T, long>(ref Unsafe.AsRef(in item)),
					_array.Length);
			}
		}

		for (var i = 0; i < _array.Length; i++)
		{
			if (EqualityComparer<T>.Default.Equals(_array[i], item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		var length = Math.Min(_array.Length, data.Length);

		_array.AsSpan(0, length).CopyTo(data);

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return TNumber.CreateChecked(_array.Length);
	}

	public readonly int Count()
	{
		return _array.Length;
	}

	public readonly long LongCount()
	{
		return _array.LongLength;
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		for (var i = 0; i < _array.Length; i++)
		{
			if (@operator.Eval(_array[i]))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		for (var i = 0; i < _array.Length; i++)
		{
			if (predicate(_array[i]))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<T, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index))
		{
			var intIndex = Int32.CreateChecked(index);

			if (intIndex < _array.Length)
			{
				item = _array[intIndex];
				return true;
			}
		}

		item = default;
		return false;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var result);
		return result;
	}

	public bool TryGetFirst(out T item)
	{
		if (_array.Length != 0)
		{
			item = _array[0];
			return true;
		}

		item = default!;
		return false;
	}

	public T First()
	{
		if (TryGetFirst(out var first))
		{
			return first;
		}

		throw new Exception("Sequence doesn't contain a element");
	}

	public T FirstOrDefault()
	{
		if (_array.Length == 0)
		{
			return default;
		}

		return _array[0];
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		for (var i = 0; i < _array.Length; i++)
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), _array[i], token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		for (var i = 0; i < _array.Length; i++)
		{
			@operator.Do(in _array[i]);
		}
	}

	public bool TryGetLast(out T item)
	{
		if (_array.Length != 0)
		{
			item = _array[^1];
			return true;
		}

		item = default!;
		return false;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new Exception("Sequence doesn't contain a element");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, ArrayEnumerator<T>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, ArrayEnumerator<T>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_array.Length == 1)
		{
			item = _array[0];
			return true;
		}

		item = default!;
		return false;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new Exception("Sequence doesn't contain one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = new T[_array.Length];

		_array.CopyTo(array, 0);

		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(_array.Length, comparer);

		for (var i = 0; i < _array.Length; i++)
		{
			set.Add(_array[i]);
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>(_array.Length);
		var span = CollectionsMarshal.AsSpan(list);

		_array.AsSpan().CopyTo(span);

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		return new PooledList<T>(_array);
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(_array.Length);

		for (var i = 0; i < _array.Length; i++)
		{
			queue.Enqueue(_array[i]);
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(_array.Length)
		{
			Count = _array.Length,
		};

		_array.CopyTo(stack._array, 0);

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(_array.Length, comparer);

		for (var i = 0; i < _array.Length; i++)
		{
			set.Add(_array[i]);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = _array.Length;
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = _array;
		return true;
	}

	public ArrayEnumerator<T> GetEnumerator()
	{
		return new ArrayEnumerator<T>(_array);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}