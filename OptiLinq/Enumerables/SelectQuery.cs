using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>
	where TOperator : ISelectOperator<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseEnumerable;

	internal SelectQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool Any()
	{
		return _baseEnumerable.Any();
	}

	public bool Contains(TResult item, IEqualityComparer<TResult>? comparer = null)
	{
		comparer ??= EqualityComparer<TResult>.Default;

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
				count++;
			}
		}

		return count;
	}

	public TResult First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return TOperator.Transform(enumerator.Current);
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public TResult FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return TOperator.Transform(enumerator.Current);
		}

		return default;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator());
	}
}