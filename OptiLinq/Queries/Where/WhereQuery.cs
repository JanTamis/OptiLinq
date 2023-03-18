using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, WhereEnumerator<T, TOperator, TBaseEnumerator>>
	where TOperator : struct, IFunction<T, bool>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : IEnumerator<T>
{
	private TBaseQuery _baseEnumerable;
	private TOperator _operator;

	internal WhereQuery(ref TBaseQuery baseEnumerable, TOperator @operator)
	{
		_baseEnumerable = baseEnumerable;
		_operator = @operator;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		while (enumerable.MoveNext())
		{
			if (_operator.Eval(enumerable.Current))
			{
				seed = func.Eval(seed, enumerable.Current);
			}
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		while (enumerable.MoveNext())
		{
			if (_operator.Eval(enumerable.Current))
			{
				seed = @operator.Eval(seed, enumerable.Current);
			}
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current) && !@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext() && _operator.Eval(enumerator.Current);
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<T>.Default.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		var i = 0;

		while (enumerator.MoveNext() && i < data.Length)
		{
			if (_operator.Eval(enumerator.Current))
			{
				data[i] = enumerator.Current;
				i++;
			}
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public int Count()
	{
		return Count<int>();
	}

	public long LongCount()
	{
		return Count<long>();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current) && predicate(enumerator.Current))
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
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
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
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				item = enumerator.Current;
				return true;
			}
		}

		item = default!;
		return false;
	}

	public T First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			}
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				@operator.Do(enumerator.Current);
			}
		}
	}

	public bool TryGetLast(out T item)
	{
		using var enumerable = _baseEnumerable.GetEnumerator();
		var isValid = false;

		while (enumerable.MoveNext())
		{
			if (_operator.Eval(enumerable.Current))
			{
				isValid = true;
			}
		}

		if (!isValid)
		{
			item = default!;
			return false;
		}

		item = enumerable.Current;

		while (enumerable.MoveNext())
		{
			item = enumerable.Current;
		}

		return true;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, WhereEnumerator<T, TOperator, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, WhereEnumerator<T, TOperator, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		item = default!;

		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext())
		{
			item = enumerable.Current;
		}

		if (enumerable.MoveNext())
		{
			item = default!;
			return false;
		}

		return true;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var builder = TryGetNonEnumeratedCount(out var count)
			? new LargeArrayBuilder<T>(count)
			: new LargeArrayBuilder<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				builder.Add(enumerator.Current);
			}
		}

		return builder.ToArray();
	}

	public T[] ToArray(out int length)
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			using var enumerator = _baseEnumerable.GetEnumerator();
			var array = new T[count];

			length = 0;

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				if (_operator.Eval(enumerator.Current))
				{
					length++;
					array[i] = enumerator.Current;
				}
			}

			return array;
		}

		return EnumerableHelper.ToArray<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = new HashSet<T>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				set.Add(enumerator.Current);
			}
		}

		return set;
	}

	public List<T> ToList()
	{
		var list = new List<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				queue.Enqueue(enumerator.Current);
			}
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				stack.Push(enumerator.Current);
			}
		}

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_operator.Eval(enumerator.Current))
			{
				set.Add(enumerator.Current);
			}
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public WhereEnumerator<T, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new WhereEnumerator<T, TOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _operator);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}