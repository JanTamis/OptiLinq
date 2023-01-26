using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator> : IOptiQuery<TResult, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>
	where TOperator : IFunction<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
	where TBaseQuery : struct, IOptiQuery<T, TBaseEnumerator>
{
	private TBaseQuery _baseEnumerable;

	internal SelectQuery(TBaseQuery baseEnumerable)
	{
		_baseEnumerable = baseEnumerable;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<TResult, bool>
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		while (enumerator.MoveNext())
		{
			if (!TAllOperator.Eval(TOperator.Eval(enumerator.Current)))
			{
				return false;
			}
		}

		return true;
	}
	
	public bool Any()
	{
		return _baseEnumerable.Any();
	}

	public IEnumerable<TResult> AsEnumerable()
	{
		return new QueryAsEnumerable<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this);
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

	public TResult ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _baseEnumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				if (index == TIndex.Zero)
				{
					return TOperator.Eval(enumerator.Current);
				}

				index--;
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
				if (index == TIndex.Zero)
				{
					return TOperator.Eval(enumerator.Current);
				}

				index--;
			}
		}

		return default;
	}

	public TResult First()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return TOperator.Eval(enumerator.Current);
		}

		throw new InvalidOperationException("The sequence doesn't contain elements");
	}

	public TResult FirstOrDefault()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (enumerator.MoveNext())
		{
			return TOperator.Eval(enumerator.Current);
		}

		return default;
	}

	public TResult Last()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		if (!enumerable.MoveNext())
		{
			throw new InvalidOperationException("The sequence doesn't contain elements");
		}

		var value = TOperator.Eval(enumerable.Current);

		while (enumerable.MoveNext())
		{
			value = TOperator.Eval(enumerable.Current);
		}

		return value;
	}

	public TResult LastOrDefault()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		var value = default(TResult);

		while (enumerable.MoveNext())
		{
			value = TOperator.Eval(enumerable.Current);
		}

		return value;
	}

	public TResult Max()
	{
		TResult? value = default;

		using var e = GetEnumerator();
		
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
				if (next != null && Comparer<TResult>.Default.Compare(next, value) > 0)
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

				if (Comparer<TResult>.Default.Compare(next, value) > 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public TResult Min()
	{
		TResult? value = default;

		using var e = GetEnumerator();

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
				if (next != null && Comparer<TResult>.Default.Compare(next, value) < 0)
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

				if (Comparer<TResult>.Default.Compare(next, value) < 0)
				{
					value = next;
				}
			}
		}

		return value;
	}

	public TResult Single()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();
		
		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return TOperator.Eval(enumerable.Current);
		}

		throw new Exception("Sequence contains to much elements");
	}

	public TResult SingleOrDefault()
	{
		using var enumerable = _baseEnumerable.GetEnumerator();

		if (enumerable.MoveNext() && !enumerable.MoveNext())
		{
			return TOperator.Eval(enumerable.Current);
		}

		return default;
	}

	public TResult[] ToArray()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			var array = new TResult[count];

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				array[i] = TOperator.Eval(enumerator.Current);
			}

			return array;
		}

		var builder = new LargeArrayBuilder<TResult>();

		while (enumerator.MoveNext())
		{
			builder.Add(TOperator.Eval(enumerator.Current));
		}
		
		return builder.ToArray();
	}

	public TResult[] ToArray(out int length)
	{
		using var enumerator = _baseEnumerable.GetEnumerator();

		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			var array = new TResult[count];
			length = 0;

			for (var i = 0; i < array.Length && enumerator.MoveNext(); i++)
			{
				length++;
				array[i] = TOperator.Eval(enumerator.Current);
			}

			return array;
		}

		return EnumerableHelper.ToArray<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, out length);
	}

	public List<TResult> ToList()
	{
		using var enumerator = _baseEnumerable.GetEnumerator();
		List<TResult> list;
		
		if (_baseEnumerable.TryGetNonEnumeratedCount(out var count))
		{
			list = new List<TResult>(count);
			var span = CollectionsMarshal.AsSpan(list);

			for (var i = 0; i < span.Length && enumerator.MoveNext(); i++)
			{
				span[i] = TOperator.Eval(enumerator.Current);
			}
		}
		else
		{
			list = new List<TResult>();

			while (enumerator.MoveNext())
			{
				list.Add(TOperator.Eval(enumerator.Current));
			}
		}

		return list;
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _baseEnumerable.TryGetNonEnumeratedCount(out length);
	}

	public SkipQuery<TCount, TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, count);
	}

	public TakeQuery<TCount, TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, count);
	}

	public OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>> Order(IComparer<TResult>? comparer = null)
	{
		return new OrderQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>>(this, comparer);
	}

	public SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator> GetEnumerator()
	{
		return new SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator>(_baseEnumerable.GetEnumerator());
	}
}