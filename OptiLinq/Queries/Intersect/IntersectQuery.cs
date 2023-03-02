using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery> : IOptiQuery<T, IntersectEnumerator<T, TComparer>>
	where TComparer : IEqualityComparer<T>
	where TFirstQuery : struct, IOptiQuery<T>
	where TSecondQuery : struct, IOptiQuery<T>
{
	private TComparer _comparer;
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	private int _count = -1;

	public IntersectQuery(TFirstQuery firstQuery, TSecondQuery secondQuery, TComparer comparer)
	{
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				seed = func.Eval(seed, enumerator.Current);
				count++;
			}
		}

		_count = count;
		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				seed = @operator.Eval(seed, enumerator.Current);
				count++;
			}
		}

		_count = count;
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current) && !@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(this);
	}

	public bool Contains<TComparer1>(T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current) && comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current) && EqualityComparer<T>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<T> data)
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var index = 0;

		while (index < data.Length && enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				data[index++] = enumerator.Current;
			}
		}

		return index;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
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

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsPositive(index))
		{
			using var set = GetSet();
			using var enumerator = _firstQuery.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (set.Remove(enumerator.Current))
				{
					if (TIndex.IsZero(index))
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

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
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
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
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
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				@operator.Do(enumerator.Current);
				_count++;
			}
		}

		_count = 0;
	}

	public bool TryGetLast(out T item)
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		item = default!;
		var count = 0;

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				item = enumerator.Current;
				count++;
				break;
			}
		}

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				item = enumerator.Current;
				count++;
			}
		}

		_count = count;
		return count > 0;
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
		throw new NotImplementedException();
	}

	public T Min()
	{
		throw new NotImplementedException();
	}

	public bool TryGetSingle(out T item)
	{
		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		item = default!;

		if (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				item = enumerator.Current;
				return true;
			}
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

		throw new InvalidOperationException("The sequence doesn't contain one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		return EnumerableHelper.ToArray<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(this, out _count);
	}

	public T[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(this, out length);
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var result = new HashSet<T>(comparer);

		using var set = GetSet();
		using var enumerator = _firstQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (set.Remove(enumerator.Current))
			{
				result.Add(enumerator.Current);
			}
		}

		return result;
	}

	public List<T> ToList()
	{
		return EnumerableHelper.ToList<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(this, out _count);
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

	public IntersectEnumerator<T, TComparer> GetEnumerator()
	{
		return new IntersectEnumerator<T, TComparer>(_firstQuery.GetEnumerator(), _secondQuery.GetEnumerator(), _comparer, _count);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();

	private PooledSet<T, TComparer> GetSet()
	{
		var set = new PooledSet<T, TComparer>(_comparer);

		using var enumerator = _secondQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Remove(enumerator.Current);
		}

		return set;
	}
}