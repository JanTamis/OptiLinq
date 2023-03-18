using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, ShuffleEnumerator<T, TBaseEnumerator>>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : IEnumerator<T>
{
	private TBaseQuery _baseQuery;
	private readonly int? _seed;

	internal ShuffleQuery(ref TBaseQuery baseQuery, int? seed)
	{
		_baseQuery = baseQuery;
		_seed = seed;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		return _baseQuery.Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(func, selector, seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		return _baseQuery.Aggregate(@operator, seed);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.All(@operator);
	}

	public bool Any()
	{
		return _baseQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.Any(@operator);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _baseQuery.Contains(in item, comparer);
	}

	public bool Contains(in T item)
	{
		return _baseQuery.Contains(in item);
	}

	public int CopyTo(Span<T> data)
	{
		var count = _baseQuery.CopyTo(data);

		Shuffle(data.Slice(0, count));

		return count;
	}

	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>();
	}

	public int Count()
	{
		return _baseQuery.Count();
	}

	public long LongCount()
	{
		return _baseQuery.LongCount();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.Count<TCountOperator, TNumber>(@operator);
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>(predicate);
	}

	public int Count(Func<T, bool> predicate)
	{
		return _baseQuery.Count(predicate);
	}

	public long CountLong(Func<T, bool> predicate)
	{
		return _baseQuery.CountLong(predicate);
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
		using var enumerator = GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
			return true;
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

		throw new InvalidOperationException("Sequence contains no elements");
	}

	public T FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		using var enumerable = GetEnumerator();

		if (!enumerable.MoveNext())
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

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public T LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T Max()
	{
		return _baseQuery.Max();
	}

	public T Min()
	{
		return _baseQuery.Min();
	}

	public bool TryGetSingle(out T item)
	{
		return _baseQuery.TryGetSingle(out item);
	}

	public T Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains no elements or more than one element");
	}

	public T SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[] ToArray()
	{
		var array = _baseQuery.ToArray();

		Shuffle(array);

		return array;
	}

	public T[] ToArray(out int length)
	{
		var array = _baseQuery.ToArray(out length);

		Shuffle(array.AsSpan(0, length));

		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		return _baseQuery.ToHashSet(comparer);
	}

	public List<T> ToList()
	{
		var list = _baseQuery.ToList();

		Shuffle(CollectionsMarshal.AsSpan(list));

		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = _baseQuery.ToPooledList();

		Shuffle(list.Items.AsSpan(0, list.Count));

		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = _baseQuery.ToPooledQueue();

		Shuffle(queue._array.AsSpan(0, queue.Count));

		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = _baseQuery.ToPooledStack();

		Shuffle(stack._array.AsSpan(0, stack.Count));

		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _baseQuery.ToPooledSet(comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseQuery.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		span = ReadOnlySpan<T>.Empty;
		return false;
	}

	public ShuffleEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		var list = _baseQuery.ToPooledList();
		Shuffle(list.Items.AsSpan(0, list.Count));

		return new ShuffleEnumerator<T, TBaseEnumerator>(list);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private void Shuffle(Span<T> data)
	{
		var random = GetRandom();

		for (var i = data.Length - 1; i >= 1; i--)
		{
			var j = random.Next(i + 1);

			(data[j], data[i]) = (data[i], data[j]);
		}
	}

	private Random GetRandom()
	{
		if (_seed is null)
		{
			return Random.Shared;
		}

		return new Random(_seed.GetValueOrDefault());
	}
}