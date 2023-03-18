using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, AppendEnumerator<T, TBaseEnumerator>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseQuery;
	private readonly T _element;

	internal AppendQuery(ref TBaseQuery baseQuery, in T element)
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
		return @operator.Eval(_baseQuery.Aggregate(@operator, seed), _element);
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.All(@operator) && @operator.Eval(_element);
	}

	public bool Any()
	{
		return true;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		return _baseQuery.Any(@operator) || @operator.Eval(_element);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return _baseQuery.Contains(item, comparer) || comparer.Equals(item, _element);
	}

	public bool Contains(in T item)
	{
		return _baseQuery.Contains(item) || EqualityComparer<T>.Default.Equals(item, _element);
	}

	public int CopyTo(Span<T> data)
	{
		var length = _baseQuery.CopyTo(data);

		if (length < data.Length)
		{
			data[length - 1] = _element;
			length++;
		}

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>() + TNumber.One;
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
		return _baseQuery.Count<TCountOperator, TNumber>(@operator) + (@operator.Eval(_element) ? TNumber.One : TNumber.Zero);
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		return _baseQuery.Count<TNumber>(predicate) + (predicate(_element) ? TNumber.One : TNumber.Zero);
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

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out T item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;
			return true;
		}

		item = _element;
		return true;
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

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		Task.Factory.StartNew(x => @operator.Do((T)x), _element, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		_baseQuery.ForEach(@operator);
		@operator.Do(_element);
	}

	public bool TryGetLast(out T item)
	{
		item = _element;
		return true;
	}

	public T Last()
	{
		return _element;
	}

	public T LastOrDefault()
	{
		return _element;
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
		throw new NotImplementedException();
	}

	public T Single()
	{
		if (Any())
		{
			throw new InvalidOperationException("Sequence contains more than one element");
		}

		return _element;
	}

	public T SingleOrDefault()
	{
		if (Any())
		{
			return default!;
		}

		return _element;
	}

	public T[] ToArray()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			var array = new T[count];

			_baseQuery.CopyTo(array);
			array[count - 1] = _element;
		}

		using var enumerator = GetEnumerator();
		using var list = new PooledList<T>(0);

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		list.Add(_element);

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

	public PooledList<T> ToPooledList()
	{
		var list = _baseQuery.ToPooledList();
		list.Add(_element);
		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = _baseQuery.ToPooledQueue();
		queue.Enqueue(_element);
		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = _baseQuery.ToPooledStack();
		stack.Push(_element);
		return stack;
	}

	public PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		var set = _baseQuery.ToPooledSet(comparer);
		set.Add(_element);
		return set;
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

	public AppendEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new AppendEnumerator<T, TBaseEnumerator>(_baseQuery.GetEnumerator(), _element);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}