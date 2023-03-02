using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery> : IOptiQuery<TResult, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>
	where TOperator : struct, IFunction<T, TSubQuery>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TSubQuery : IOptiQuery<TResult>
{
	private TBaseQuery _baseQuery;
	private TOperator _selector;

	internal SelectManyQuery(ref TBaseQuery baseQuery, TOperator selector)
	{
		_baseQuery = baseQuery;
		_selector = selector;
	}

	public TResult1 Aggregate<TFunc, TResultSelector, TAccumulate, TResult1>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate> where TResultSelector : struct, IFunction<TAccumulate, TResult1>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var result = seed;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			result = subQuery.Aggregate(func, result);
		}

		return selector.Eval(result);
	}

	public TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, TResult, TAccumulate>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var result = seed;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			result = subQuery.Aggregate(@operator, result);
		}

		return result;
	}

	public bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (!subQuery.All(@operator))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Any())
			{
				return true;
			}
		}

		return false;
	}

	public bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<TResult, bool>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Any(@operator))
			{
				return true;
			}
		}

		return false;
	}

	public IEnumerable<TResult> AsEnumerable()
	{
		return new QueryAsEnumerable<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(this);
	}

	public bool Contains<TComparer>(TResult item, TComparer comparer) where TComparer : IEqualityComparer<TResult>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Contains(item, comparer))
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
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Contains(item))
			{
				return true;
			}
		}

		return false;
	}

	public int CopyTo(Span<TResult> data)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var index = 0;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			index += subQuery.CopyTo(data.Slice(index));
		}

		return index;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var count = TNumber.Zero;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			count += subQuery.Count<TNumber>();
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
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			using var subEnumerator = _selector.Eval(enumerator.Current).GetEnumerator();

			while (subEnumerator.MoveNext())
			{
				if (TIndex.IsZero(index))
				{
					item = subEnumerator.Current;
					return true;
				}

				index--;
			}
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

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public TResult ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		TryGetElementAt(index, out var item);
		return item;
	}

	public bool TryGetFirst(out TResult item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.TryGetFirst(out item))
			{
				return true;
			}
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

		throw new InvalidOperationException();
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
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			subQuery.ForEach(@operator);
		}
	}

	public bool TryGetLast(out TResult item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			item = default!;
			return false;
		}

		var subQuery = _selector.Eval(enumerator.Current);

		while (enumerator.MoveNext())
		{
			var temp = _selector.Eval(enumerator.Current);

			if (temp.Any())
			{
				subQuery = temp;
			}
		}

		return subQuery.TryGetLast(out item);
	}

	public TResult Last()
	{
		if (TryGetLast(out var item))
		{
			return item;
		}

		throw new InvalidOperationException("Sequence contains no elements.");
	}

	public TResult LastOrDefault()
	{
		TryGetLast(out var item);
		return item;
	}

	public TResult Max()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var max = default(TResult);
		var hasValue = false;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Any())
			{
				var subMax = subQuery.Max();

				if (hasValue)
				{
					if (Comparer<TResult>.Default.Compare(subMax, max) > 0)
					{
						max = subMax;
					}
				}
				else
				{
					max = subMax;
					hasValue = true;
				}
			}
		}

		if (hasValue)
		{
			return max;
		}

		throw new InvalidOperationException();
	}

	public TResult Min()
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var min = default(TResult);
		var hasValue = false;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Any())
			{
				var subMin = subQuery.Min();

				if (hasValue)
				{
					if (Comparer<TResult>.Default.Compare(subMin, min) < 0)
					{
						min = subMin;
					}
				}
				else
				{
					min = subMin;
					hasValue = true;
				}
			}
		}

		if (hasValue)
		{
			return min;
		}

		throw new InvalidOperationException();
	}

	public bool TryGetSingle(out TResult item)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.Any())
			{
				return subQuery.TryGetSingle(out item);
			}
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

		throw new InvalidOperationException("Sequence does not contain exactly one element.");
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
		return EnumerableHelper.ToArray<TResult, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(this, out length);
	}

	public HashSet<TResult> ToHashSet(IEqualityComparer<TResult>? comparer = default)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		var set = new HashSet<TResult>(comparer);

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			using var subEnumerator = subQuery.GetEnumerator();

			while (subEnumerator.MoveNext())
			{
				set.Add(subEnumerator.Current);
			}
		}

		return set;
	}

	public List<TResult> ToList()
	{
		if (TryGetNonEnumeratedCount(out var count))
		{
			var list = new List<TResult>(count);
			CopyTo(CollectionsMarshal.AsSpan(list));

			return list;
		}

		using var enumerator = _baseQuery.GetEnumerator();

		var result = new List<TResult>();

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.TryGetNonEnumeratedCount(out var subCount))
			{
				count = result.Count;
				result.EnsureCapacity(result.Count + subCount);

				subQuery.CopyTo(CollectionsMarshal.AsSpan(result).Slice(count));
			}
			else
			{
				using var subEnumerator = subQuery.GetEnumerator();

				while (subEnumerator.MoveNext())
				{
					result.Add(subEnumerator.Current);
				}
			}
		}

		return result;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		using var enumerator = _baseQuery.GetEnumerator();

		length = 0;

		while (enumerator.MoveNext())
		{
			var subQuery = _selector.Eval(enumerator.Current);

			if (subQuery.TryGetNonEnumeratedCount(out var subLength))
			{
				length += subLength;
			}
			else
			{
				length = 0;
				return false;
			}
		}

		return true;
	}

	public bool TryGetSpan(out ReadOnlySpan<TResult> span)
	{
		span = ReadOnlySpan<TResult>.Empty;
		return false;
	}

	public SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator> GetEnumerator()
	{
		return new SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>(_baseQuery.GetEnumerator(), _selector);
	}

	IOptiEnumerator<TResult> IOptiQuery<TResult>.GetEnumerator() => GetEnumerator();

	public override string ToString()
	{
		return ToString(", ");
	}

	public string ToString(string separator)
	{
		return EnumerableHelper.Join<TResult, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(GetEnumerator(), separator);
	}
}