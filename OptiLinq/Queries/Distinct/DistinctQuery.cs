using System.Buffers;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<T, DistinctEnumerator<T, TBaseEnumerator, TComparer>>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : IEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private TBaseQuery _baseQuery;
	private readonly TComparer _comparer;

	private int _count = -1;

	internal DistinctQuery(ref TBaseQuery baseQuery, TComparer comparer)
	{
		_baseQuery = baseQuery;
		_comparer = comparer;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TAccumulate seed, TFunc func = default, TResultSelector selector = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				seed = func.Eval(in seed, enumerator.Current);
			}
		}

		_count = set.Count;
		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				seed = @operator.Eval(in seed, enumerator.Current);
			}
		}

		_count = set.Count;
		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && !@operator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		_count = set.Count;
		return true;
	}

	public bool Any()
	{
		return _baseQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

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
		return _baseQuery.Contains(in item, comparer);
	}

	public bool Contains(in T item)
	{
		return _baseQuery.Contains(in item);
	}

	public int CopyTo(Span<T> data)
	{
		var index = 0;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext() && index < data.Length)
		{
			if (set.Add(enumerator.Current))
			{
				data[index++] = enumerator.Current;
			}
		}

		return index;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var length = TNumber.Zero;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				length++;
			}
		}

		_count = set.Count;
		return length;
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
		var length = TNumber.Zero;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				length++;
			}
		}

		return length;
	}

	public TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

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
		return _baseQuery.TryGetFirst(out item);
	}

	public T First()
	{
		return _baseQuery.First();
	}

	public T FirstOrDefault()
	{
		return _baseQuery.FirstOrDefault();
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			}
		}

		_count = set.Count;

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				@operator.Do(enumerator.Current);
			}
		}
	}

	public bool TryGetLast(out T item)
	{
		item = default!;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		if (!enumerator.MoveNext())
		{
			return false;
		}

		do
		{
			if (set.Add(enumerator.Current))
			{
				item = enumerator.Current;
			}
		} while (enumerator.MoveNext());

		return true;
	}

	public T Last()
	{
		return _baseQuery.Last();
	}

	public T LastOrDefault()
	{
		return _baseQuery.LastOrDefault();
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
		return _baseQuery.Single();
	}

	public T SingleOrDefault()
	{
		return _baseQuery.SingleOrDefault();
	}

	public T[] ToArray()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			using var enumerator = _baseQuery.GetEnumerator();
			using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

			var index = 0;
			var array = GC.AllocateUninitializedArray<T>(count);

			ref var first = ref MemoryMarshal.GetArrayDataReference(array);

			while (enumerator.MoveNext() && index < count)
			{
				if (set.Add(enumerator.Current))
				{
					Unsafe.Add(ref first, index++) = enumerator.Current;
				}
			}

			return array;
		}

		return EnumerableHelper.ToArray(GetEnumerator(), ArrayPool<T>.Shared, count, out _);
	}

	public T[] ToArray(out int length)
	{
		var array = ToArray();

		length = array.Length;
		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var hashSet = new HashSet<T>(comparer);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			hashSet.Add(enumerator.Current);
		}

		_count = hashSet.Count;
		return hashSet;
	}

	public List<T> ToList()
	{
		var list = new List<T>(Math.Max(_count, 4));

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		_count = list.Count;
		return list;
	}

	public PooledList<T> ToPooledList()
	{
		var list = new PooledList<T>(Math.Max(_count, 4));

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		_count = list.Count;
		return list;
	}

	public PooledQueue<T> ToPooledQueue()
	{
		var queue = new PooledQueue<T>(Math.Max(_count, 4));

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				queue.Enqueue(enumerator.Current);
			}
		}

		_count = queue.Count;
		return queue;
	}

	public PooledStack<T> ToPooledStack()
	{
		var stack = new PooledStack<T>(Math.Max(_count, 4));

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.Add(enumerator.Current))
			{
				stack.Push(enumerator.Current);
			}
		}

		_count = stack.Count;
		return stack;
	}

	public PooledSet<T, TComparer1> ToPooledSet<TComparer1>(TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		var set = new PooledSet<T, TComparer1>(Math.Max(_count, 4), comparer);
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		_count = set.Count;
		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count != -1)
		{
			length = _count;
			return true;
		}

		if (_baseQuery.TryGetNonEnumeratedCount(out length))
		{
			_count = length;
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

	public DistinctEnumerator<T, TBaseEnumerator, TComparer> GetEnumerator()
	{
		return new DistinctEnumerator<T, TBaseEnumerator, TComparer>(_baseQuery.GetEnumerator(), _comparer, Math.Max(_count, 4));
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private PooledSet<T, TComparer> GetSet()
	{
		return new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);
	}
}