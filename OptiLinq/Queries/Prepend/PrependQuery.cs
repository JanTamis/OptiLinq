using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, PrependEnumerator<T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseQuery;
	private readonly T _element;

	internal PrependQuery(ref TBaseQuery baseQuery, in T element)
	{
		_baseQuery = baseQuery;
		_element = element;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return selector.Eval(Aggregate(func, seed));
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _baseQuery.Aggregate(@operator, @operator.Eval(seed, _element));
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(_element) && _baseQuery.All(@operator);
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return @operator.Eval(_element) || _baseQuery.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return comparer.Equals(item, _element) || _baseQuery.Contains(item, comparer);
	}

	public bool Contains(T item)
	{
		return EqualityComparer<T>.Default.Equals(item, _element) || _baseQuery.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		var length = 0;

		if (data.Length > 0)
		{
			data[0] = _element;
			length++;
		}

		return length + _baseQuery.CopyTo(data.Slice(length));
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>() + TNumber.One;
	}

	public int Count()
	{
		return _baseQuery.Count() + 1;
	}

	public long LongCount()
	{
		return _baseQuery.LongCount() + 1L;
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		if (TIndex.IsZero(index))
		{
			item = _element;
			return true;
		}

		return _baseQuery.TryGetElementAt(index - TIndex.One, out item);
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
		item = _element;
		return true;
	}

	public T First()
	{
		return _element;
	}

	public T FirstOrDefault()
	{
		return _element;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		Task.Factory.StartNew(x => @operator.Do((T)x), _element, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		@operator.Do(_element);
		_baseQuery.ForEach(@operator);
	}

	public bool TryGetLast(out T item)
	{
		if (!_baseQuery.TryGetLast(out item))
		{
			item = _element;
		}

		return true;
	}

	public T Last()
	{
		if (!_baseQuery.TryGetLast(out var item))
		{
			item = _element;
		}

		return item;
	}

	public T LastOrDefault()
	{
		if (!_baseQuery.TryGetLast(out var item))
		{
			item = _element;
		}

		return item;
	}

	public T Max()
	{
		var max = _baseQuery.Max();

		if (Comparer<T>.Default.Compare(_element, max) > 0)
		{
			max = _element;
		}

		return max;
	}

	public T Min()
	{
		var min = _baseQuery.Min();

		if (Comparer<T>.Default.Compare(_element, min) < 0)
		{
			min = _element;
		}

		return min;
	}

	public bool TryGetSingle(out T item)
	{
		if (_baseQuery.Any())
		{
			item = default!;
			return false;
		}

		item = _element;
		return true;
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains more than one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			var array = new T[count];
			CopyTo(array);

			return array;
		}

		using var enumerator = _baseQuery.GetEnumerator();
		using var list = new PooledList<T>(1);

		list.Add(_element);

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list.ToArray();
	}

	public T[] ToArray(out int length)
	{
		var array = ToArray();
		length = array.Length;
		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var hashSet = _baseQuery.ToHashSet(comparer);
		hashSet.Add(_element);
		return hashSet;
	}

	public List<T> ToList()
	{
		var list = _baseQuery.ToList();
		list.Add(_element);
		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_baseQuery.TryGetNonEnumeratedCount(out length))
		{
			length++;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public PrependEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new PrependEnumerator<T, TBaseEnumerator>(_baseQuery.GetEnumerator(), _element);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();
}