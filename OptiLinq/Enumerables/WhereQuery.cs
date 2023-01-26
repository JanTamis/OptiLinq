using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>
	where TOperator : IFunction<T, bool>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseQuery _baseEnumerable;

	internal WhereQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TOperator.Eval(enumerator.Current) && !TAllOperator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext() && TOperator.Eval(enumerator.Current);
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>(this);
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
				if (TOperator.Eval(enumerator.Current))
				{
					count++;
				}
			}
		}

		return count;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (TOperator.Eval(enumerator.Current))
				{
					if (index == TIndex.Zero)
					{
						return enumerator.Current;
					}

					index--;
				}
			}
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (TOperator.Eval(enumerator.Current))
				{
					if (index == TIndex.Zero)
					{
						return enumerator.Current;
					}

					index--;
				}
			}
		}

		return default;
	}

	public T First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TOperator.Eval(enumerator.Current))
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
			if (TOperator.Eval(enumerator.Current))
			{
				return enumerator.Current;
			}
		}

		return default;
	}

	public T Last()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();
		var isValid = false;

		while (enumerable.MoveNext())
		{
			if (TOperator.Eval(enumerable.Current))
			{
				isValid = true;
			}
		}
		
		if (!isValid)
		{
			throw new InvalidOperationException("The sequence doesn't contain elements");
		}

		var value = enumerable.Current;

		while (enumerable.MoveNext())
		{
			value = enumerable.Current;
		}

		return value;
	}

	public T LastOrDefault()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		var value = default(T);

		while (enumerable.MoveNext())
		{
			if (TOperator.Eval(enumerable.Current))
			{
				value = enumerable.Current;
			}
		}

		return value;
	}

	public T Max()
	{
		T? value = default;

		using var e = _baseEnumerable.GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!e.MoveNext())
				{
					return value;
				}

				value = e.Current;
			} while (value == null);

			while (e.MoveNext())
			{
				var next = e.Current;
				if (next != null && TOperator.Eval(next) && Comparer<T>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!e.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = e.Current;

			while (e.MoveNext())
			{
				var next = e.Current;

				if (TOperator.Eval(next) && Comparer<T>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public T Min()
	{
		T? value = default;

		using var e = _baseEnumerable.GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!e.MoveNext())
				{
					return value;
				}

				value = e.Current;
			} while (value == null);

			while (e.MoveNext())
			{
				var next = e.Current;
				if (next != null && TOperator.Eval(next) && Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!e.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = e.Current;

			while (e.MoveNext())
			{
				var next = e.Current;

				if (TOperator.Eval(next) && Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public T Single()
	{
		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public T SingleOrDefault()
	{
		using var enumerable = GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		return default;
	}

	public T[] ToArray()
	{
		LargeArrayBuilder<T> builder = new();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TOperator.Eval(enumerator.Current))
			{
				builder.Add(enumerator.Current);
			}
		}

		return builder.ToArray();
	}

	public T[] ToArray(out int length)
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			using var enumerator = _baseEnumerable.GetEnumerator();
			var array = new T[count];

			length = 0;

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				if (TOperator.Eval(enumerator.Current))
				{
					length++;
					array[i] = enumerator.Current;
				}
			}

			return array;
		}

		return EnumerableHelper.ToArray<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>(this, out length);
	}

	public List<T> ToList()
	{
		var list = new List<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (TOperator.Eval(enumerator.Current))
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TSelectOperator, TResult>() where TSelectOperator : IFunction<T, TResult>
	{
		return new WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
	}

	public WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TSelectOperator>() where TSelectOperator : IFunction<T, T>
	{
		return new WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(_baseEnumerable);
	}

	public SkipQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>(this, count);
	}

	public TakeQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>(this, count);
	}

	public OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>> Order(IComparer<T>? comparer = null)
	{
		return new OrderQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereOperatorEnumerator<T, TOperator, TBaseEnumerator>>(this, comparer);
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