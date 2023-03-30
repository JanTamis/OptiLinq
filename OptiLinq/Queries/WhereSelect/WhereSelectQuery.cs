using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>
	where TWhereOperator : struct, IFunction<T, bool>
	where TSelectOperator : struct, IFunction<T, TResult>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : IEnumerator<T>
{
	private TBaseQuery _baseEnumerable;

	private TWhereOperator _whereOperator;
	private TSelectOperator _selectOperator;

	internal WhereSelectQuery(ref TBaseQuery baseEnumerable, TWhereOperator whereOperator, TSelectOperator selectOperator)
	{
		_baseEnumerable = baseEnumerable;
		_whereOperator = whereOperator;
		_selectOperator = selectOperator;
	}

	public TResult1 Aggregate<TFunc, TResultSelector, TAccumulate, TResult1>(TAccumulate seed, TFunc func = default, TResultSelector selector = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult1>
	{
		if (_baseEnumerable.TryGetSpan(out var data))
		{
			foreach (var item in data)
			{
				if (_whereOperator.Eval(in item))
				{
					seed = func.Eval(in seed, _selectOperator.Eval(in item));
				}
			}
		}
		else
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;

				if (_whereOperator.Eval(in current))
				{
					seed = func.Eval(in seed, _selectOperator.Eval(in current));
				}
			}
		}

		return selector.Eval(seed);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TAccumulate seed, TFunc @operator = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
	{
		if (_baseEnumerable.TryGetSpan(out var data))
		{
			foreach (var item in data)
			{
				if (_whereOperator.Eval(in item))
				{
					seed = @operator.Eval(in seed, _selectOperator.Eval(in item));
				}
			}
		}
		else
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;

				if (_whereOperator.Eval(in current))
				{
					seed = @operator.Eval(in seed, _selectOperator.Eval(in current));
				}
			}
		}

		return seed;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current) && !@operator.Eval(_selectOperator.Eval(enumerator.Current)))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current) && @operator.Eval(_selectOperator.Eval(enumerator.Current)))
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
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current) && comparer.Equals(_selectOperator.Eval(enumerator.Current), item))
			{
				return true;
			}
		}

		return false;
	}

	public bool Contains(in TResult item)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current) && EqualityComparer<TResult>.Default.Equals(_selectOperator.Eval(enumerator.Current), item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<TResult> data)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		var i = 0;

		while (i < data.Length && enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				data[i++] = _selectOperator.Eval(enumerator.Current);
			}
		}

		return i;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;
		var tempOperator = _whereOperator;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (tempOperator.Eval(enumerator.Current))
			{
				count++;
			}
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
		var count = TNumber.Zero;
		var tempOperator = _whereOperator;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (tempOperator.Eval(enumerator.Current) && @operator.Eval(_selectOperator.Eval(enumerator.Current)))
			{
				count++;
			}
		}

		return count;
	}

	public TNumber Count<TNumber>(Func<TResult, bool> predicate) where TNumber : INumberBase<TNumber>
	{
		var count = TNumber.Zero;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current) && predicate(_selectOperator.Eval(enumerator.Current)))
			{
				count++;
			}
		}

		return count;
	}

	public int Count(Func<TResult, bool> predicate)
	{
		return Count<int>(predicate);
	}

	public long CountLong(Func<TResult, bool> predicate)
	{
		return Count<long>(predicate);
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

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public TResult ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out TResult item)
	{
		return EnumerableHelper.TryGetFirst(GetEnumerator(), out item);
	}

	public TResult First()
	{
		if (TryGetFirst(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public TResult FirstOrDefault()
	{
		TryGetFirst(out var item);
		return item;
	}

	public Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<TResult>
	{
		var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, Environment.ProcessorCount);
		using var enumerator = _baseEnumerable.GetEnumerator();

		var selectOperator = _selectOperator;

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				Task.Factory.StartNew(x => @operator.Do(selectOperator.Eval((T)x)), enumerator.Current, token, TaskCreationOptions.None, schedulerPair.ConcurrentScheduler);
			}
		}

		schedulerPair.Complete();
		return schedulerPair.Completion;
	}

	public void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<TResult>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				@operator.Do(_selectOperator.Eval(enumerator.Current));
			}
		}
	}

	public bool TryGetLast(out TResult item)
	{
		return EnumerableHelper.TryGetLast(GetEnumerator(), out item);
	}

	public TResult Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public TResult LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public TResult Max()
	{
		return EnumerableHelper.Max<TResult, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(GetEnumerator());
	}

	public TResult Min()
	{
		return EnumerableHelper.Min<TResult, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(GetEnumerator());
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
		return ToArray(out _);
	}

	public TResult[] ToArray(out int length)
	{
		using var list = new PooledList<TResult>();
		using var enumerator = GetEnumerator();

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		length = list.Count;
		return list.ToArray();
	}

	public HashSet<TResult> ToHashSet(IEqualityComparer<TResult>? comparer = default)
	{
		var set = new HashSet<TResult>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				set.Add(_selectOperator.Eval(enumerator.Current));
			}
		}

		return set;
	}

	public List<TResult> ToList()
	{
		var list = new List<TResult>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				list.Add(_selectOperator.Eval(enumerator.Current));
			}
		}

		return list;
	}

	public PooledList<TResult> ToPooledList()
	{
		var list = new PooledList<TResult>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				list.Add(_selectOperator.Eval(enumerator.Current));
			}
		}

		return list;
	}

	public PooledQueue<TResult> ToPooledQueue()
	{
		var queue = new PooledQueue<TResult>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				queue.Enqueue(_selectOperator.Eval(enumerator.Current));
			}
		}

		return queue;
	}

	public PooledStack<TResult> ToPooledStack()
	{
		var stack = new PooledStack<TResult>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				stack.Push(_selectOperator.Eval(enumerator.Current));
			}
		}

		return stack;
	}

	public PooledSet<TResult, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<TResult>
	{
		var set = new PooledSet<TResult, TComparer>(comparer);

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (_whereOperator.Eval(enumerator.Current))
			{
				set.Add(_selectOperator.Eval(enumerator.Current));
			}
		}

		return set;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public bool TryGetSpan(out ReadOnlySpan<TResult> span)
	{
		span = ReadOnlySpan<TResult>.Empty;
		return false;
	}

	public WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> GetEnumerator()
	{
		return new WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _whereOperator, _selectOperator);
	}

	IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}