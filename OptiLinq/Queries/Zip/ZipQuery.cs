using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery> : IOptiQuery<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>
	where TOperator : struct, IFunction<T, T, TResult>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TSecondQuery : struct, IOptiQuery<T>
	where TFirstEnumerator : struct, IOptiEnumerator<T>
{
	private readonly TOperator _operator;
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	internal ZipQuery(TOperator @operator, in TFirstQuery firstQuery, in TSecondQuery secondQuery)
	{
		_operator = @operator;
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
	}

	public TResult1 Aggregate<TFunc, TResultSelector, TAccumulate, TResult1>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult1>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = func.Eval(seed, enumerator.Current);
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(seed, enumerator.Current);
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<TResult, bool>
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
		return _firstQuery.Any() && _secondQuery.Any();
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<TResult, bool>
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

	public IEnumerable<TResult> AsEnumerable()
	{
		throw new NotImplementedException();
	}

	public bool Contains<TComparer>(TResult item, TComparer comparer) where TComparer : IEqualityComparer<TResult>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!comparer.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(TResult item)
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!EqualityComparer<TResult>.Default.Equals(enumerator.Current, item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<TResult> data)
	{
		var count = 0;

		if (!data.IsEmpty)
		{
			using var enumerator = GetEnumerator();

			while (count < data.Length && enumerator.MoveNext())
			{
				data[count++] = enumerator.Current;
			}
		}

		return count;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
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

	public bool TryGetElementAt<TIndex>(TIndex index, out TResult item) where TIndex : IBinaryInteger<TIndex>
	{
		return EnumerableHelper.TryGetElementAt(GetEnumerator(), index, out item);
	}

	public TResult ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (TryGetElementAt(index, out var item))
		{
			return item;
		}

		throw ThrowHelper.CreateOutOfRangeException();
	}

	public TResult ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out TResult item)
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

	public TResult First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new Exception("Sequence contains no elements");
	}

	public TResult FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<TResult>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			Task.Factory.StartNew(x => @operator.Do((TResult)x), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<TResult>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			@operator.Do(enumerator.Current);
		}
	}

	public bool TryGetLast(out TResult item)
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

	public TResult Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new Exception("Sequence contains no elements");
	}

	public TResult LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public TResult Max()
	{
		return EnumerableHelper.Max<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(GetEnumerator());
	}

	public TResult Min()
	{
		return EnumerableHelper.Min<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(GetEnumerator());
	}

	public bool TryGetSingle(out TResult item)
	{
		item = default!;

		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext())
		{
			item = enumerable.Current;
		}

		if (enumerable.MoveNext())
		{
			item = default!;
			return false;
		}

		return true;
	}

	public TResult Single()
	{
		if (TryGetSingle(out var item))
		{
			return item;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public TResult SingleOrDefault()
	{
		TryGetSingle(out var item);
		return item;
	}

	public TResult[] ToArray()
	{
		return EnumerableHelper.ToArray<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(this, out _);
	}

	public TResult[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(this, out length);
	}

	public HashSet<TResult> ToHashSet(IEqualityComparer<TResult>? comparer = default)
	{
		var set = new HashSet<TResult>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
	}

	public List<TResult> ToList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new List<TResult>(count)
			: new List<TResult>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_firstQuery.TryGetNonEnumeratedCount(out var firstLength) && _secondQuery.TryGetNonEnumeratedCount(out var secondLength))
		{
			length = Math.Min(firstLength, secondLength);
			return true;
		}

		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<TResult> span)
	{
		span = ReadOnlySpan<TResult>.Empty;
		return false;
	}

	public ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>> GetEnumerator()
	{
		return new ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>(in _operator, _firstQuery.GetEnumerator(), _secondQuery.GetEnumerator());
	}

	IOptiEnumerator<TResult> IOptiQuery<TResult>.GetEnumerator() => GetEnumerator();
}