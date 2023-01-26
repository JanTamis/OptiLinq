using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, SkipEnumerator<TCount, T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseQuery _baseEnumerable;
	private readonly TCount _count;

	internal SkipQuery(TBaseQuery baseEnumerable, TCount count)
	{
		_baseEnumerable = baseEnumerable;
		_count = count;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			if (!TAllOperator.Eval(enumerator.Current))
			{
				return false;
			}
		}

		return true;
	}

	public bool Any()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		return enumerator.MoveNext();
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

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

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
			}

			while (enumerator.MoveNext())
			{
				count++;
			}
		}
		else
		{
			count -= Int32.CreateChecked(_count);
		}

		return count;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
			}

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					return enumerator.Current;
				}

				index--;
			}
		}

		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
			}

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					return enumerator.Current;
				}

				index--;
			}
		}

		return default;
	}

	public T First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return enumerator.Current;
		}

		throw new InvalidOperationException("The sequence is empty");
	}

	public T FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			return enumerator.Current;
		}

		return default;
	}

	public T Last()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		var isValid = false;

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext())
		{
			isValid = true;
		}

		if (!isValid)
		{
			throw new InvalidOperationException("The sequence doesn't contain elements");
		}

		var value = enumerator.Current;

		while (enumerator.MoveNext())
		{
			value = enumerator.Current;
		}

		return value;
	}

	public T LastOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		var value = default(T);

		while (enumerator.MoveNext())
		{
			value = enumerator.Current;
		}

		return value;
	}

	public T Max()
	{
		T? value = default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (value is null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (next is not null && Comparer<T>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!enumerator.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = enumerator.Current;

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (Comparer<T>.Default.Compare(next, value) > 0)
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

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (value is null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (next is not null && Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}
		else
		{
			if (!enumerator.MoveNext())
			{
				throw new Exception("Collection is empty");
			}

			value = enumerator.Current;

			while (enumerator.MoveNext())
			{
				var next = enumerator.Current;

				if (Comparer<T>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public T Single()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext() && !enumerator.MoveNext())
		{
			return enumerator.Current;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public T SingleOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		if (enumerator.MoveNext() && !enumerator.MoveNext())
		{
			return enumerator.Current;
		}

		return default;
	}

	public T[] ToArray()
	{
		LargeArrayBuilder<T> builder = new();

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public List<T> ToList()
	{
		var list = new List<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
		}

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public WhereQuery<T, TWhereOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Where<TWhereOperator>() where TWhereOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, TResult, TSelectOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Select<TResult, TSelectOperator>() where TSelectOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, T, TSelectOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Select<TSelectOperator>() where TSelectOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public SkipTakeQuery<TCount, TTakeCount, T, TBaseQuery, TBaseEnumerator> Take<TTakeCount>(TTakeCount count)
		where TTakeCount : IBinaryInteger<TTakeCount>
	{
		return new SkipTakeQuery<TCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, _count, count);
	}

	public OrderQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Order(IComparer<T>? comparer = null)
	{
		return new OrderQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(this, comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public SkipEnumerator<TCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new SkipEnumerator<TCount, T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _count);
	}
}