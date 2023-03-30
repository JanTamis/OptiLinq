using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery> : IOptiQuery<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>
	where TOperator : struct, IFunction<T, T, TResult>
	where TFirstQuery : struct, IOptiQuery<T, TFirstEnumerator>
	where TSecondQuery : struct, IOptiQuery<T>
	where TFirstEnumerator : IEnumerator<T>
{
	private TOperator _operator;
	private TFirstQuery _firstQuery;
	private TSecondQuery _secondQuery;

	internal ZipQuery(TOperator @operator, in TFirstQuery firstQuery, in TSecondQuery secondQuery)
	{
		_operator = @operator;
		_firstQuery = firstQuery;
		_secondQuery = secondQuery;
	}

	public TResult1 Aggregate<TFunc, TResultSelector, TAccumulate, TResult1>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult1>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = func.Eval(in seed, enumerator.Current);
		}

		return selector.Eval(in seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
	{
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			seed = @operator.Eval(in seed, enumerator.Current);
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
		return this;
	}

	public bool Contains<TComparer>(in TResult item, TComparer comparer) where TComparer : IEqualityComparer<TResult>
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

	public bool Contains(in TResult item)
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

	public TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default) where TNumber : INumberBase<TNumber> where TCountOperator : struct, IFunction<TResult, bool>
	{
		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
		{
			if (@operator.Eval(_operator.Eval(firstEnumerator.Current, secondEnumerator.Current)))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<TResult, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var firstEnumerator = _firstQuery.GetEnumerator();
		using var secondEnumerator = _secondQuery.GetEnumerator();

		while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
		{
			if (predicate(_operator.Eval(firstEnumerator.Current, secondEnumerator.Current)))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<TResult, bool> predicate)
	{
		throw new NotImplementedException();
	}

	public long CountLong(Func<TResult, bool> predicate)
	{
		throw new NotImplementedException();
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
		return EnumerableHelper.Max<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(GetEnumerator());
	}

	public TResult Min()
	{
		return EnumerableHelper.Min<TResult, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(GetEnumerator());
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
		return EnumerableHelper.ToArray<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(this, out _);
	}

	public TResult[] ToArray(out int length)
	{
		return EnumerableHelper.ToArray<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(this, out length);
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

	public PooledList<TResult> ToPooledList()
	{
		var list = TryGetNonEnumeratedCount(out var count)
			? new PooledList<TResult>(count)
			: new PooledList<TResult>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public PooledQueue<TResult> ToPooledQueue()
	{
		var queue = TryGetNonEnumeratedCount(out var count)
			? new PooledQueue<TResult>(count)
			: new PooledQueue<TResult>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			queue.Enqueue(enumerator.Current);
		}

		return queue;
	}

	public PooledStack<TResult> ToPooledStack()
	{
		var stack = TryGetNonEnumeratedCount(out var count)
			? new PooledStack<TResult>(count)
			: new PooledStack<TResult>();

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			stack.Push(enumerator.Current);
		}

		return stack;
	}

	public PooledSet<TResult, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<TResult>
	{
		var set = new PooledSet<TResult, TComparer>(comparer);

		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			set.Add(enumerator.Current);
		}

		return set;
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

	public ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>> GetEnumerator()
	{
		return new ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>(in _operator, _firstQuery.GetEnumerator(), _secondQuery.GetEnumerator());
	}

	IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}