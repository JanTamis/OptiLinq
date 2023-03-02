using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>
	where TOperator : struct, IFunction<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseQuery;
	private TOperator _operator;

	internal SelectQuery(ref TBaseQuery baseQuery, TOperator @operator)
	{
		_baseQuery = baseQuery;
		_operator = @operator;
	}

	public TResult1 Aggregate<TFunc, TResultSelector, TAccumulate, TResult1>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult1>
	{
		using var enumerable = _baseQuery.GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = func.Eval(seed, _operator.Eval(enumerable.Current));
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
	{
		using var enumerable = _baseQuery.GetEnumerator();

		while (enumerable.MoveNext())
		{
			seed = @operator.Eval(seed, _operator.Eval(enumerable.Current));
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!@operator.Eval(_operator.Eval(enumerator.Current)))
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

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (@operator.Eval(_operator.Eval(enumerator.Current)))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<TResult> AsEnumerable()
	{
		return new QueryAsEnumerable<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this);
	}

	public bool Contains<TComparer>(TResult item, TComparer comparer) where TComparer : IEqualityComparer<TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (comparer.Equals(item, _operator.Eval(enumerator.Current)))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(TResult item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (EqualityComparer<TResult>.Default.Equals(item, _operator.Eval(enumerator.Current)))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<TResult> data)
	{
		using var enumerator = _baseQuery.GetEnumerator();
		ref var first = ref MemoryMarshal.GetReference(data);

		var i = 0;

		for (; i < data.Length && enumerator.MoveNext(); i++)
		{
			Unsafe.Add(ref first, i) = _operator.Eval(enumerator.Current);
		}

		return i;
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

	public bool TryGetElementAt<TIndex>(TIndex index, out TResult item) where TIndex : IBinaryInteger<TIndex>
	{
		if (_baseQuery.TryGetElementAt(index, out var baseItem))
		{
			item = _operator.Eval(baseItem);
			return true;
		}

		item = default!;
		return false;
	}

	public TResult ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public TResult ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out TResult item)
	{
		if (_baseQuery.TryGetFirst(out var baseItem))
		{
			item = _operator.Eval(baseItem);
			return true;
		}

		item = default!;
		return false;
	}

	public TResult First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public TResult FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<TResult>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseQuery.GetEnumerator();

		var currentOperator = _operator;

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do(currentOperator.Eval((T)x)), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(_operator.Eval(enumerator.Current));
		}
	}

	public bool TryGetLast(out TResult item)
	{
		if (_baseQuery.TryGetLast(out var baseItem))
		{
			item = _operator.Eval(baseItem);
			return true;
		}

		item = default!;
		return false;
	}

	public TResult Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public TResult LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public TResult Max()
	{
		return EnumerableHelper.Max<TResult, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(GetEnumerator());
	}

	public TResult Min()
	{
		return EnumerableHelper.Min<TResult, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(GetEnumerator());
	}

	public bool TryGetSingle(out TResult item)
	{
		if (_baseQuery.TryGetSingle(out var baseItem))
		{
			item = _operator.Eval(baseItem);
			return true;
		}

		item = default!;
		return false;
	}

	public TResult Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence does not contain exactly one element");
	}

	public TResult SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public TResult[] ToArray()
	{
		return ToArray(out _);
	}

	public TResult[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, out length);
	}

	public HashSet<TResult> ToHashSet(IEqualityComparer<TResult>? comparer = default)
	{
		comparer ??= EqualityComparer<TResult>.Default;

		var set = _baseQuery.TryGetNonEnumeratedCount(out var count)
			? new HashSet<TResult>(count, comparer)
			: new HashSet<TResult>(comparer);

		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(_operator.Eval(enumerator.Current));
		}

		return set;
	}

	public List<TResult> ToList()
	{
		using var enumerator = _baseQuery.GetEnumerator();
		List<TResult> list;

		if (_baseQuery.TryGetNonEnumeratedCount(out var count))
		{
			list = new List<TResult>(count);
			var span = CollectionsMarshal.AsSpan(list);

			for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
			{
				span[i] = _operator.Eval(enumerator.Current);
			}
		}
		else
		{
			list = new List<TResult>();

			while (enumerator.MoveNext())
			{
				list.Add(_operator.Eval(enumerator.Current));
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseQuery.TryGetNonEnumeratedCount(out length);
	}

	public bool TryGetSpan(out ReadOnlySpan<TResult> span)
	{
		span = ReadOnlySpan<TResult>.Empty;
		return false;
	}

	public SelectEnumerator<T, TResult, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>(_baseQuery.GetEnumerator(), _operator);
	}

	IOptiEnumerator<TResult> IOptiQuery<TResult>.GetEnumerator()
	{
		return GetEnumerator();
	}
}