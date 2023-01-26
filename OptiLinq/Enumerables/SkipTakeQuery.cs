using System.Numerics;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TSkipCount : IBinaryInteger<TSkipCount>
	where TTakeCount : IBinaryInteger<TTakeCount>
{
	private TBaseQuery _baseEnumerable;
	private readonly TSkipCount _skipCount;
	private readonly TTakeCount _takeCount;

	internal SkipTakeQuery(TBaseQuery baseEnumerable, TSkipCount skipCount, TTakeCount takeCount)
	{
		_baseEnumerable = baseEnumerable;
		_skipCount = skipCount;
		_takeCount = takeCount;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		return enumerator.MoveNext();
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

			for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
			{
			}

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
			{
				count++;
			}
		}
		else
		{
			count -= Int32.CreateChecked(_skipCount);
		}

		return count;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
			{
			}

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

			for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
			{
			}

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			value = enumerator.Current;
		}

		return value;
	}

	public T LastOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{
		}

		var value = default(T);

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			value = enumerator.Current;
		}

		return value;
	}

	public T Max()
	{
		T? value = default;

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

			for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
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

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{

		}

		for (var i = TTakeCount.Zero; i < _takeCount && enumerator.MoveNext(); i++)
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public List<T> ToList()
	{
		var list = new List<T>();

		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TSkipCount.Zero; i < _skipCount && enumerator.MoveNext(); i++)
		{

		}

		while (enumerator.MoveNext())
		{
			list.Add(enumerator.Current);
		}

		return list;
	}

	public WhereQuery<T, TWhereOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Where<TWhereOperator>() where TWhereOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, TResult, TSelectOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Select<TResult, TSelectOperator>() where TSelectOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, T, TSelectOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Select<TSelectOperator>() where TSelectOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this);
	}

	public OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Order(IComparer<T>? comparer = null)
	{
		return new OrderQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this, comparer);
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _skipCount, _takeCount);
	}
}