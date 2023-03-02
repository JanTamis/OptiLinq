using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, TakeEnumerator<TCount, T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseQuery _baseQuery;
	private readonly TCount _count;

	internal TakeQuery(ref TBaseQuery baseQuery, TCount count)
	{
		_baseQuery = baseQuery;
		_count = count;
	}

	public TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		if (_count == TCount.Zero)
		{
			return false;
		}

		if (_baseQuery.TryGetNonEnumeratedCount(out _))
		{
			return true;
		}

		using var enumerator = _baseQuery.GetEnumerator();

		return enumerator.MoveNext();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			if (@operator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(T item, TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			if (comparer.Equals(item, enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(T item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		using var enumerator = _baseQuery.GetEnumerator();

		var i = 0;
		var length = Math.Min(data.Length, Int32.CreateSaturating(_count));

		for (; i < length && enumerator.MoveNext(); i++)
		{
			data[i] = enumerator.Current;
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var enumerator = _baseQuery.GetEnumerator();
		var count = TNumber.Zero;

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			count++;
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

	public bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			if (TIndex.IsZero(index))
			{
				item = enumerator.Current;
				return true;
			}

			index--;
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
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		return default!;
	}

	public bool TryGetFirst(out T item)
	{
		if (TCount.IsZero(_count))
		{
			item = default!;
			return false;
		}

		return _baseQuery.TryGetFirst(out item);
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
		if (TryGetFirst(out var item))
		{
			return item;
		}

		return default!;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			Task.Factory.StartNew(x => @operator.Do((T)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out T item)
	{
		if (TCount.IsZero(_count))
		{
			item = default!;
			return false;
		}

		using var enumerator = _baseQuery.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		item = enumerator.Current;

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			item = enumerator.Current;
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
		return EnumerableHelper.Max<T, TakeEnumerator<TCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public T Min()
	{
		return EnumerableHelper.Min<T, TakeEnumerator<TCount, T, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out T item)
	{
		throw new NotImplementedException();
	}

	public T Single()
	{
		using var enumerable = _baseQuery.GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public T SingleOrDefault()
	{
		using var enumerable = _baseQuery.GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		return default;
	}

	public T[] ToArray()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var array = new T[Int32.CreateChecked(_count)];

		var i = 0;

		for (; i < array.Length && enumerator.MoveNext(); i++)
		{
			array[i] = enumerator.Current;
		}

		if (i != array.Length)
		{
			return RuntimeHelpers.GetSubArray(array, new Range(0, i));
		}

		return array;
	}

	public T[] ToArray(out int length)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var array = new T[Int32.CreateChecked(_count)];

		var i = 0;

		for (; i < array.Length && enumerator.MoveNext(); i++)
		{
			array[i] = enumerator.Current;
		}

		length = i;
		return array;
	}

	public HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default)
	{
		var set = _baseQuery.TryGetNonEnumeratedCount(out var count)
			? new HashSet<T>(Math.Min(count, Int32.CreateTruncating(_count)), comparer)
			: new HashSet<T>(comparer);

		using var enumerator = _baseQuery.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<T> ToList()
	{
		using var enumerator = _baseQuery.GetEnumerator();
		List<T> list;

		if (_baseQuery.TryGetNonEnumeratedCount(out var count))
		{
			list = new List<T>(Int32.Min(count, Int32.CreateChecked(_count)));
			var span = CollectionsMarshal.AsSpan(list);

			for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
			{
				span[i] = enumerator.Current;
			}
		}
		else
		{
			list = new List<T>(Int32.CreateChecked(_count));

			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_baseQuery.TryGetNonEnumeratedCount(out var baseCount))
		{
			length = Int32.Min(baseCount, Int32.CreateChecked(_count));
			return true;
		}

		length = Int32.CreateChecked(_count);
		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<T> span)
	{
		if (_baseQuery.TryGetSpan(out var baseSpan))
		{
			span = baseSpan.Slice(0, Int32.Min(baseSpan.Length, Int32.CreateChecked(_count)));
			return true;
		}

		span = default;
		return false;
	}

	public TakeEnumerator<TCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new TakeEnumerator<TCount, T, TBaseEnumerator>(_baseQuery.GetEnumerator(), _count);
	}

	IOptiEnumerator<T> IOptiQuery<T>.GetEnumerator()
	{
		return GetEnumerator();
	}
}