using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public readonly struct GenerateQuery<T, TOperator> : IOptiQuery<T, GenerateEnumerator<T, TOperator>> where TOperator : IFunction<T, T>
{
	private readonly T _parameter;

	internal GenerateQuery(T parameter)
	{
		_parameter = parameter;
	}

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public bool Any()
	{
		return true;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer)
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public int Count()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = GetEnumerator();

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
			using var enumerator = GetEnumerator();

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
		return _parameter;
	}

	public T FirstOrDefault()
	{
		return _parameter;
	}

	public T Last()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T LastOrDefault()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T Max()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T Min()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T Single()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T SingleOrDefault()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public T[] ToArray()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public List<T> ToList()
	{
		throw new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}

	public WhereQuery<T, TWhereOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Where<TWhereOperator>() where TWhereOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this);
	}

	public SelectQuery<T, TResult, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Select<TSelectOperator, TResult>() where TSelectOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this);
	}

	public SelectQuery<T, T, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Select<TSelectOperator>() where TSelectOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this);
	}

	public SkipQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, count);
	}

	public TakeQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, count);
	}

	public OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Order(IComparer<T>? comparer = null)
	{
		return new OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(this, comparer);
	}

	public GenerateEnumerator<T, TOperator> GetEnumerator()
	{
		return new GenerateEnumerator<T, TOperator>(_parameter);
	}
}