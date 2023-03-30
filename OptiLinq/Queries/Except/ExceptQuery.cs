using System.Collections;
using System.Numerics;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery> : IOptiQuery<T, ExceptEnumerator<T, TFirstEnumerator, TComparer>>
	where TComparer : IEqualityComparer<T>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TFirstEnumerator : IEnumerator<T>
	where TSecondQuery : struct, IOptiQuery<T>
{
	private readonly TComparer _comparer;
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	private int _count = -1;

	public ExceptQuery(TFirstQuery firstQuery, TSecondQuery secondQuery, TComparer comparer)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = set.Count;

		while (enumerator.MoveNext())
		{
			var current = enumerator.Current;

			if (set.Add(current))
			{
				seed = func.Eval(in seed, in current);
			}
		}

		_count = set.Count - count;
		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = set.Count;

		while (enumerator.MoveNext())
		{
			var current = enumerator.Current;

			if (set.Add(current))
			{
				seed = @operator.Eval(in seed, in current);
			}
		}

		_count = set.Count - count;
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && !@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && @operator.Eval(enumerator.Current))
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

	public bool Contains<TComparer1>(in T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in T item)
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && EqualityComparer<T>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var index = 0;

		while (index < data.Length && enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				data[index++] = enumerator.Current;
			}
		}

		return index;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				count++;
			}
		}

		_count = Int32.CreateTruncating(count);
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
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && predicate(enumerator.Current))
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
			using var set = _secondQuery.ToPooledSet(_comparer);
			using var enumerator = _firstQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (set.Add(enumerator.Current))
				{
					if (index == TIndex.Zero)
					{
						item = enumerator.Current;
						return true;
					}

					index--;
				}
			}
		}

		item = default!;
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
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
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

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
				count++;
			}
		}

		_count = 0;

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				@operator.Do(enumerator.Current);
				_count++;
			}
		}

		_count = 0;
	}

	public bool TryGetLast(out T item)
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		item = default!;
		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				item = enumerator.Current;
				count++;
				break;
			}
		}

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;
		count++;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				item = enumerator.Current;
				count++;
			}
		}

		_count = count;

		return true;
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		item = default!;

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				item = enumerator.Current;
				break;
			}
		}

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

		if (enumerator.MoveNext())
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

		throw new InvalidOperationException("The sequence doesn't contain one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		return EnumerableHelper.ToArray<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(this, out _count);
	}

	public T[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var result = new HashSet<T>(comparer);

		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				result.Add(enumerator.Current);
			}
		}

		return result;
	}

	public List<T> ToList()
	{
		return EnumerableHelper.ToList<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(this, out _count);
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>();

		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>();

		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				queue.Enqueue(enumerator.Current);
			}
		}

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>();

		using var set = _secondQuery.ToPooledSet(_comparer);
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				stack.Push(enumerator.Current);
			}
		}

		return stack;
	}

	public PooledSet<T, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		var set = _secondQuery.ToPooledSet(comparer);

		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count is not -1)
		{
			length = _count;
			return true;
		}

		length = default;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public ExceptEnumerator<T, TFirstEnumerator, TComparer> GetEnumerator()
	{
		return new ExceptEnumerator<T, TFirstEnumerator, TComparer>(_firstQuery.GetEnumerator(), _secondQuery.ToPooledSet(_comparer));
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}