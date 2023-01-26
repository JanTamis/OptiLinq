using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator> : IOptiQuery<T, TakeEnumerator<TCount, T, TBaseEnumerator>>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
	where TCount : IBinaryInteger<TCount>
{
	private TBaseQuery _baseEnumerable;
	private readonly TCount _count;

	internal TakeQuery(TBaseQuery baseEnumerable, TCount count)
	{
		_baseEnumerable = baseEnumerable;
		_count = count;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		if (_count == TCount.Zero)
		{
			return false;
		}

		if (_baseEnumerable.TryGetNonEnumeratedCount(out _))
		{
			return true;
		}

		using var enumerator = _baseEnumerable.GetEnumerator();

		return enumerator.MoveNext();
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		comparer ??= EqualityComparer<T>.Default;
		using var enumerator = _baseEnumerable.GetEnumerator();

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		using var enumerator = _baseEnumerable.GetEnumerator();
		var count = 0;

		for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
		{
			count++;
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

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public T FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return enumerator.Current;
		}

		return default;
	}

	public T Last()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (!enumerator.MoveNext())
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

		var value = default(T);

		while (enumerator.MoveNext())
		{
			value = enumerator.Current;
		}

		return value;
	}

	public T Max()
	{
		var value = default(T);

		using var enumerator = GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
				var next = enumerator.Current;
				if (next != null && Comparer<T>.Default.Compare(next, value) > 0)
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

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		var value = default(T);

		using var enumerator = GetEnumerator();

		if (value == null)
		{
			do
			{
				if (!enumerator.MoveNext())
				{
					return value;
				}

				value = enumerator.Current;
			} while (value == null);

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
			{
				var next = enumerator.Current;
				if (next != null && Comparer<T>.Default.Compare(next, value) < 0)
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

			for (var i = TCount.Zero; i < _count && enumerator.MoveNext(); i++)
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
		using var enumerable = _baseEnumerable.GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		throw new Exception("Sequence contains to much elements");
	}

	public T SingleOrDefault()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return enumerable.Current;
		}

		return default;
	}

	public T[] ToArray()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			var array = new T[Int32.Min(count, Int32.CreateChecked(_count))];

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				array[i] = enumerator.Current;
			}

			return array;
		}

		var builder = new LargeArrayBuilder<T>(Int32.CreateChecked(_count));

		while (enumerator.MoveNext())
		{
			builder.Add(enumerator.Current);
		}

		return builder.ToArray();
	}

	public List<T> ToList()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		List<T> list;

		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			list = new List<T>(Int32.Min(count, Int32.CreateChecked(_count)));
			var span = CollectionsMarshal.AsSpan(list);

			for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
			{
				span[i] = enumerator.Current;
			}
		}
		else
		{
			list = new List<T>(Int32.CreateChecked(_count));

			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var baseCount))
		{
			length = Int32.Min(baseCount, Int32.CreateChecked(_count));
			return true;
		}

		length = 0;
		return false;
	}

	public WhereQuery<T, TWhereOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Where<TWhereOperator>() where TWhereOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, TResult, TSelectOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Select<TResult, TSelectOperator>() where TSelectOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public SelectQuery<T, T, TSelectOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Select<TSelectOperator>() where TSelectOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this);
	}

	public OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Order(IComparer<T>? comparer = null)
	{
		return new OrderQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(this, comparer);
	}

	public SkipTakeQuery<TSkipCount, TCount, T, TBaseQuery, TBaseEnumerator> Take<TSkipCount>(TSkipCount count)
		where TSkipCount : IBinaryInteger<TSkipCount>
	{
		return new SkipTakeQuery<TSkipCount, TCount, T, TBaseQuery, TBaseEnumerator>(_baseEnumerable, count, _count);
	}

	public TakeEnumerator<TCount, T, TBaseEnumerator> GetEnumerator()
	{
		return new TakeEnumerator<TCount, T, TBaseEnumerator>(_baseEnumerable.GetEnumerator(), _count);
	}
}