using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, TakeLastEnumerator<T, TBaseEnumerator>>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseQuery;

	private int _totalCount = -1;
	private readonly int _count;

	public TakeLastQuery(TBaseQuery baseQuery, int count)
	{
		_baseQuery = baseQuery;
		_count = count;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			seed = func.Eval(seed, result);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			seed = @operator.Eval(seed, result);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			if (!@operator.Eval(result))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _baseQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			if (@operator.Eval(result))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var queue = GetQueue();

		return queue.Contains(item, comparer);
	}

	public bool Contains(T item)
	{
		using var queue = GetQueue();

		return queue.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		using var queue = GetQueue();

		return queue.CopyTo(data);
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var queue = new PooledQueue<T>(_count);
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (queue.Count == _count)
			{
				break;
			}

			queue.Enqueue(enumerator.Current);
		}

		return TNumber.CreateChecked(queue.Count);
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
			using var queue = GetQueue();

			while (queue.TryDequeue(out var result))
			{
				if (TIndex.IsZero(index))
				{
					item = result;
					return true;
				}

				index--;
			}
		}

		item = default!;
		return false;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var result))
		{
			return result;
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
		using var queue = GetQueue();

		return queue.TryDequeue(out item!);
	}

	public T First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), result, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var queue = GetQueue();

		while (queue.TryDequeue(out var result))
		{
			@operator.Do(result);
		}
	}

	public bool TryGetLast(out T item)
	{
		if (_count is 0)
		{
			item = default!;
			return false;
		}

		return _baseQuery.TryGetLast(out item);
	}

	public T Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence was empty");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return EnumerableHelper.Max<T, TakeLastEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, TakeLastEnumerator<T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		if (_count is 1)
		{
			return _baseQuery.TryGetLast(out item);
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

		throw new InvalidOperationException("Sequence does not contain exactly one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		using var queue = GetQueue();

		return queue.ToArray();
	}

	public T[] ToArray(out int length)
	{
		using var queue = GetQueue();

		length = _totalCount;
		return queue.ToArray();
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		using var queue = GetQueue();
		var set = new HashSet<T>(comparer);

		while (queue.TryDequeue(out var result))
		{
			set.Add(result);
		}

		return set;
	}

	public List<T> ToList()
	{
		using var queue = GetQueue();
		var list = new List<T>(_totalCount);

		queue.CopyTo(CollectionsMarshal.AsSpan(list));

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_totalCount is not -1)
		{
			length = _totalCount;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseQuery.TryGetSpan(out span))
		{
			span = span[^_count..];
			return true;
		}

		return false;
	}

	public TakeLastEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new TakeLastEnumerator<T, TBaseEnumerator>(_baseQuery.GetEnumerator(), _count);
	}

	IOptiEnumerator<T> IOptiQuery<T>.
		GetEnumerator() => GetEnumerator();

	private PooledQueue<T> GetQueue()
	{
		var queue = TryGetNonEnumeratedCount(out var count)
			? new PooledQueue<T>(count)
			: new PooledQueue<T>(_count);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (queue.Count == _count)
			{
				queue.Dequeue();
			}

			queue.Enqueue(enumerator.Current);
		}

		_totalCount = queue.Count;
		return queue;
	}
}