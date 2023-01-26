using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>
	where TWhereOperator : IFunction<T, bool>
	where TSelectOperator : IFunction<T, TResult>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseEnumerable;

	internal WhereSelectQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<TResult, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current) && !TAllOperator.Eval(TSelectOperator.Eval(enumerator.Current)))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext() && TWhereOperator.Eval(enumerator.Current);
	}

	public IEnumerable<TResult> AsEnumerable()
	{
		return new QueryAsEnumerable<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this);
	}

	public bool Contains(TResult item, IEqualityComparer<TResult>? comparer = null)
	{
		comparer ??= EqualityComparer<TResult>.Default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current) && comparer.Equals(TSelectOperator.Eval(enumerator.Current), item))
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
				if (TWhereOperator.Eval(enumerator.Current))
				{
					count++;
				}
			}
		}

		return count;
	}

	public TResult ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (TWhereOperator.Eval(enumerator.Current))
				{
					if (index == TIndex.Zero)
					{
						return TSelectOperator.Eval(enumerator.Current);
					}

					index--;
				}
			}
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public TResult ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (TWhereOperator.Eval(enumerator.Current))
				{
					if (index == TIndex.Zero)
					{
						return TSelectOperator.Eval(enumerator.Current);
					}

					index--;
				}
			}
		}

		return default;
	}

	public TResult First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current))
			{
				return TSelectOperator.Eval(enumerator.Current);
			}
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public TResult FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current))
			{
				return TSelectOperator.Eval(enumerator.Current);
			}
		}

		return default;
	}

	public TResult Last()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();
		var isValid = false;

		while (enumerable.MoveNext())
		{
			if (TWhereOperator.Eval(enumerable.Current))
			{
				isValid = true;
			}
		}

		if (!isValid)
		{
			throw new InvalidOperationException("The sequence doesn't contain elements");
		}

		var value = TSelectOperator.Eval(enumerable.Current);

		while (enumerable.MoveNext())
		{
			value = TSelectOperator.Eval(enumerable.Current);
		}

		return value;
	}

	public TResult LastOrDefault()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		var value = default(TResult);

		while (enumerable.MoveNext())
		{
			if (TWhereOperator.Eval(enumerable.Current))
			{
				value = TSelectOperator.Eval(enumerable.Current);
			}
		}

		return value;
	}

	public TResult Max()
	{
		TResult? value = default;

		using var e = _baseEnumerable.GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!e.MoveNext())
				{
					return value;
				}

				value = TSelectOperator.Eval(e.Current);
			} while (value == null);

			while (e.MoveNext())
			{
				var next = e.Current;

				if (next != null && TWhereOperator.Eval(next))
				{
					var temp = TSelectOperator.Eval(next);

					if (Comparer<TResult>.Default.Compare(value, temp) > 0)
					{
						value = temp;
					}
				}
			}
		}
		else
		{
			while (e.MoveNext())
			{
				if (TWhereOperator.Eval(e.Current))
				{
					value = TSelectOperator.Eval(e.Current);
					break;
				}
			}

			while (e.MoveNext())
			{
				var next = e.Current;

				if (TWhereOperator.Eval(next))
				{
					var temp = TSelectOperator.Eval(next);

					if (Comparer<TResult>.Default.Compare(value, temp) > 0)
					{
						value = temp;
					}
				}
			}
		}

		return value;
	}

	public TResult Min()
	{
		TResult? value = default;

		using var e = _baseEnumerable.GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!e.MoveNext())
				{
					return value;
				}

				value = TSelectOperator.Eval(e.Current);
			} while (value == null);

			while (e.MoveNext())
			{
				var next = e.Current;

				if (next != null && TWhereOperator.Eval(next))
				{
					var temp = TSelectOperator.Eval(next);

					if (Comparer<TResult>.Default.Compare(value, temp) < 0)
					{
						value = temp;
					}
				}
			}
		}
		else
		{
			while (e.MoveNext())
			{
				if (TWhereOperator.Eval(e.Current))
				{
					value = TSelectOperator.Eval(e.Current);
					break;
				}
			}

			while (e.MoveNext())
			{
				var next = e.Current;

				if (TWhereOperator.Eval(next))
				{
					var temp = TSelectOperator.Eval(next);

					if (Comparer<TResult>.Default.Compare(value, temp) < 0)
					{
						value = temp;
					}
				}
			}
		}

		return value;
	}

	public TResult Single()
	{
		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public TResult SingleOrDefault()
	{
		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		return default;
	}

	public TResult[] ToArray()
	{
		LargeArrayBuilder<TResult> builder = new();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current))
			{
				builder.Add(TSelectOperator.Eval(enumerator.Current));
			}
		}

		return builder.ToArray();
	}

	public List<TResult> ToList()
	{
		var list = new List<TResult>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TWhereOperator.Eval(enumerator.Current))
			{
				list.Add(TSelectOperator.Eval(enumerator.Current));
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public WhereQuery<TResult, TOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Where<TOperator>() 
		where TOperator : IFunction<TResult, bool>
	{
		return new WhereQuery<TResult, TOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this);
	}

	public WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> GetEnumerator()
	{
		return new WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator());
	}

	public SkipQuery<TCount, TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this, count);
	}

	public TakeQuery<TCount, TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this, count);
	}

	public OrderQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Order(IComparer<TResult>? comparer = null)
	{
		return new OrderQuery<TResult, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this, comparer);
	}
}