using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T[], ChunkEnumerator<T, TBaseEnumerator>>
	where TBaseEnumerator : IEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseEnumerable;
	private readonly int _chunkSize;

	public ChunkQuery(ref TBaseQuery baseEnumerable, int chunkSize)
	{
		_baseEnumerable = baseEnumerable;
		_chunkSize = chunkSize;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T[], TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerable = GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = func.Eval(seed, enumerable.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T[], TAccumulate>
	{
		using var enumerable = GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = @operator.Eval(seed, enumerable.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T[], bool>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		return _baseEnumerable.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T[], bool>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T[]> AsEnumerable()
	{
		return this;
	}

	public bool Contains<TComparer>(in T[] item, TComparer comparer) where TComparer : IEqualityComparer<T[]>
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

	public bool Contains(in T[] item)
	{
		return Contains(item, EqualityComparer<T[]>.Default);
	}

	public int CopyTo(Span<T[]> data)
	{
		using var enumerable = GetEnumerator();

		var length = 0;

		for (var i = 0; i < data.Length && enumerable.MoveNext(); i++)
		{
			data[i] = enumerable.Current;
			length++;
		}

		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		return _baseEnumerable.Count<TNumber>() / TNumber.CreateChecked(_chunkSize);
	}

	public int Count()
	{
		return Count<int>();
	}

	public long LongCount()
	{
		return Count<long>();
	}

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<T[], bool>
	{
		var count = TNumber.Zero;

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<T[], bool> predicate) where TNumber : INumberBase<TNumber>
	{
		using var enumerator = GetEnumerator();
		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			if (predicate(enumerator.Current))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<T[], bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<T[], bool> predicate)
	{
		return Count<long>(predicate);
	}

	public bool TryGetElementAt<TIndex>(TIndex index, out T[] item) where TIndex : IBinaryInteger<TIndex>
	{
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
	}

	public T[] ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T[] ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var result);
		return result;
	}

	public bool TryGetFirst(out T[] item)
	{
		return EnumerableHelper.TryGetLast(GetEnumerator(), out item);
	}

	public T[] First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public T[] FirstOrDefault()
	{
		TryGetFirst(out var result);
		return result;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T[]>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do(x as T[]), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T[]>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T[] item)
	{
		return EnumerableHelper.TryGetLast(GetEnumerator(), out item);
	}

	public T[] Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public T[] LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public T[] Max()
	{
		throw new NotImplementedException();
	}

	public T[] Min()
	{
		throw new NotImplementedException();
	}

	public bool TryGetSingle(out T[] item)
	{
		using var enumerator = GetEnumerator();

		if (enumerator.MoveNext())
		{
			item = enumerator.Current;

			if (!enumerator.MoveNext())
			{
				return true;
			}
		}

		item = Array.Empty<T>();
		return false;
	}

	public T[] Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain exactly one element");
	}

	public T[] SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public T[][] ToArray()
	{
		var builder = TryGetNonEnumeratedCount(out var count)
			? new LargeArrayBuilder<T[]>(count)
			: new LargeArrayBuilder<T[]>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public T[][] ToArray(out int length)
	{
		var array = ToArray();
		length = array.Length;

		return array;
	}

	public HashSet<T[]> ToHashSet(IEqualityComparer<T[]>? comparer = default)
	{
		comparer ??= EqualityComparer<T[]>.Default;

		var set = new HashSet<T[]>(comparer);

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T[]> ToList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new List<T[]>(count)
			: new List<T[]>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledList<T[]> ToPooledList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new PooledList<T[]>(count)
			: new PooledList<T[]>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledQueue<T[]> ToPooledQueue()
	{
		var queue = TryGetNonEnumeratedCount(out var count)
			? new PooledQueue<T[]>(count)
			: new PooledQueue<T[]>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			queue.Enqueue(enumerator.Current);
		}

		return queue;
	}

	public PooledStack<T[]> ToPooledStack()
	{
		var stack = TryGetNonEnumeratedCount(out var count)
			? new PooledStack<T[]>(count)
			: new PooledStack<T[]>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			stack.Push(enumerator.Current);
		}

		return stack;
	}

	public PooledSet<T[], TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T[]>
	{
		var set = new PooledSet<T[], TComparer>(comparer);

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			length = count / _chunkSize;
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<T[]> span)
	{
		span = ReadOnlySpan<T[]>.Empty;
		return false;
	}

	public ChunkEnumerator<T, TBaseEnumerator> GetEnumerator()
	{
		return new ChunkEnumerator<T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _chunkSize);
	}

	IEnumerator<T[]> IEnumerable<T[]>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}