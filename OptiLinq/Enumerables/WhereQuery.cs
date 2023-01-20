using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>
	where TOperator : IWhereOperator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseEnumerable;

	internal WhereQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext() && TOperator.IsAccepted(enumerator.Current);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;

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

	public int Count()
	{
		if (!TryGetNonEnumeratedCount(out var count))
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (TOperator.IsAccepted(enumerator.Current))
				{
					count++;
				}
			}
		}

		return count;
	}

	public T First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
    
    while (enumerator.MoveNext())
    {
	    if (TOperator.IsAccepted(enumerator.Current))
	    {
		    return enumerator.Current;
	    }
    }

    throw new InvalidOperationException("The sequence is empty");
	}

	public T FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TOperator.IsAccepted(enumerator.Current))
			{
				return enumerator.Current;
			}
		}

		return default;
	}

	public WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TResult, TSelectOperator>() where TSelectOperator : ISelectOperator<T, TResult>
	{
		return new WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
	}

	public WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TSelectOperator>() where TSelectOperator : ISelectOperator<T, T>
	{
		return new WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public WhereOperatorEnumerator<T, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator());
	}
}