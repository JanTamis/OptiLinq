using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer> : IOptiQuery<T, DistinctEnumerator<T, TBaseEnumerator, TComparer>>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
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

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();
		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				seed = func.Eval(seed, enumerator.Current);
				_count++;
			}
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				seed = @operator.Eval(seed, enumerator.Current);

				_count++;
			}
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && !@operator.Eval(enumerator.Current))
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
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = GetSet();

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current) && @operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator, TComparer>>(this);
	}

	public bool Contains<TComparer1>(T item, TComparer1 comparer) where TComparer1 : IEqualityComparer<T>
	{
		return _baseQuery.Contains(item, comparer);
	}

	public bool Contains(T item)
	{
		return _baseQuery.Contains(item);
	}

	public int CopyTo(Span<T> data)
	{
		var index = 0;

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		while (enumerator.MoveNext() && index < data.Length)
		{
			if (set.AddIfNotPresent(enumerator.Current))
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

		if (typeof(TNumber) == typeof(int) || typeof(TNumber) == typeof(long))
		{
			_count = 0;
		}

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				length++;
			}
		}

		if (typeof(TNumber) == typeof(int) || typeof(TNumber) == typeof(long))
		{
			_count = Int32.CreateChecked(length);
		}

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

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);

				_count++;
			}
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), _comparer);

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				@operator.Do(enumerator.Current);

				_count++;
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
			if (set.AddIfNotPresent(enumerator.Current))
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
			using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), ArrayPool<int>.Shared, ArrayPool<Slot<T>>.Shared, _comparer);

			var index = 0;
			var array = GC.AllocateUninitializedArray<T>(count);

			ref var first = ref MemoryMarshal.GetArrayDataReference(array);

			while (enumerator.MoveNext() && index < count)
			{
				if (set.AddIfNotPresent(enumerator.Current))
				{
					Unsafe.Add(ref first, index++) = enumerator.Current;
				}
			}

			return array;
		}

		if (_baseQuery.TryGetNonEnumeratedCount(out count))
		{
			using var enumerator = _baseQuery.GetEnumerator();
			using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), ArrayPool<int>.Shared, ArrayPool<Slot<T>>.Shared, _comparer);

			var index = 0;
			var array = ArrayPool<T>.Shared.Rent(count);

			ref var first = ref MemoryMarshal.GetArrayDataReference(array);

			while (enumerator.MoveNext())
			{
				if (set.AddIfNotPresent(enumerator.Current))
				{
					Unsafe.Add(ref first, index++) = enumerator.Current;
				}
			}

			var result = GC.AllocateUninitializedArray<T>(index);
			array.AsSpan(0, index).CopyTo(result);

			ArrayPool<T>.Shared.Return(array);

			return result;
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

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (hashSet.Add(enumerator.Current))
			{
				_count++;
			}
		}

		return hashSet;
	}

	public List<T> ToList()
	{
		var list = new List<T>(Math.Max(_count, 4));

		using var enumerator = _baseQuery.GetEnumerator();
		using var set = new PooledSet<T, TComparer>(Math.Max(_count, 0), ArrayPool<int>.Shared, ArrayPool<Slot<T>>.Shared, _comparer);

		_count = 0;

		while (enumerator.MoveNext())
		{
			if (set.AddIfNotPresent(enumerator.Current))
			{
				list.Add(enumerator.Current);
				_count++;
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_count != -1)
		{
			length = _count;
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

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator() => GetEnumerator();

	private PooledSet<T, TComparer> GetSet()
	{
		return new PooledSet<T, TComparer>(Math.Max(_count, 0), ArrayPool<int>.Shared, ArrayPool<Slot<T>>.Shared, _comparer);
	}
}