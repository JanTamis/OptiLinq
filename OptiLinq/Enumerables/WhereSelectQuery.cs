using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>
	where TWhereOperator : IWhereOperator<T>
	where TSelectOperator : ISelectOperator<T, TResult>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseEnumerable;

	internal WhereSelectQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext() && TWhereOperator.IsAccepted(enumerator.Current);
	}

	public bool Contains(TResult item, IEqualityComparer<TResult>? comparer = null)
	{
		comparer ??= EqualityComparer<TResult>.Default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.IsAccepted(enumerator.Current) && comparer.Equals(TSelectOperator.Transform(enumerator.Current), item))
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
				if (TWhereOperator.IsAccepted(enumerator.Current))
				{
					count++;
				}
			}
		}

		return count;
	}

	public TResult First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.IsAccepted(enumerator.Current))
			{
				return TSelectOperator.Transform(enumerator.Current);
			}
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public TResult FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.IsAccepted(enumerator.Current))
			{
				return TSelectOperator.Transform(enumerator.Current);
			}
		}

		return default;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public WhereQuery<TResult, TOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Where<TOperator>() where TOperator : IWhereOperator<TResult>
	{
		return new WhereQuery<TResult, TOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this);
	}

	public WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> GetEnumerator()
	{
		return new WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator());
	}
}