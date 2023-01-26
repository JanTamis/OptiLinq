using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public readonly struct EnumerableQuery<T> : IOptiQuery<T, EnumerableEnumerator<T>>
{
	private readonly IEnumerable<T> _enumerable;

	internal EnumerableQuery(IEnumerable<T> enumerable)
	{
		_enumerable = enumerable;
	}

	public static implicit operator EnumerableQuery<T>(T[] enumerable) => new EnumerableQuery<T>(enumerable);

	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		using var enumerator = _enumerable.GetEnumerator();

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
		return _enumerable.Any();
	}

	public IEnumerable<T> AsEnumerable()
	{
		return _enumerable;
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
	{
		return _enumerable.Contains(item, comparer);
	}

	public int Count()
	{
		return _enumerable.Count();
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		if (index >= TIndex.Zero)
		{
			using var enumerator = _enumerable.GetEnumerator();
			
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
			using var enumerator = _enumerable.GetEnumerator();

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
		return _enumerable.First();
	}

	public T FirstOrDefault()
	{
		return _enumerable.FirstOrDefault();
	}

	public T Last()
	{
		return _enumerable.Last();
	}

	public T LastOrDefault()
	{
		return _enumerable.LastOrDefault();
	}

	public T Max()
	{
		return _enumerable.Max();
	}

	public T Min()
	{
		return _enumerable.Min();
	}

	public T Single()
	{
		return _enumerable.Single();
	}

	public T SingleOrDefault()
	{
		return _enumerable.SingleOrDefault();
	}

	public T[] ToArray()
	{
		return _enumerable.ToArray();
	}

	public List<T> ToList()
	{
		return _enumerable.ToList();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		return _enumerable.TryGetNonEnumeratedCount(out length);
	}

	public WhereQuery<T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>> Where<TOperator>() where TOperator : IFunction<T, bool>
	{
		return new WhereQuery<T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>>(this);
	}

	public SelectQuery<T, TResult, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>> Select<TOperator, TResult>() where TOperator : IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>>(this);
	}

	public SelectQuery<T, T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>> Select<TOperator>() where TOperator : IFunction<T, T>
	{
		return new SelectQuery<T, T, TOperator, EnumerableQuery<T>, EnumerableEnumerator<T>>(this);
	}

	public SkipQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>>(this, count);
	}

	public TakeQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>>(this, count);
	}

	public EnumerableEnumerator<T> GetEnumerator()
	{
		return new EnumerableEnumerator<T>(_enumerable.GetEnumerator());
	}
}