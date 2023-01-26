using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct EmptyQuery<T> : IOptiQuery<T, EmptyEnumerator<T>>
{
	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		return false;
	}

	public bool Any()
	{
		return false;
	}

	public IEnumerable<T> AsEnumerable()
	{
		return new QueryAsEnumerable<T, EmptyQuery<T>, EmptyEnumerator<T>>(this);
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer)
	{
		return false;
	}

	public int Count()
	{
		return 0;
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		throw new IndexOutOfRangeException("Index was out of bounds");
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		return default;
	}

	public T First()
	{
		return default;
	}

	public T FirstOrDefault()
	{
		return default;
	}

	public T Last()
	{
		throw new Exception("Sequence doesn't contain elements");
	}

	public T LastOrDefault()
	{
		return default;
	}

	public T Max()
	{
		return default;
	}

	public T Min()
	{
		return default;
	}

	public T Single()
	{
		throw new Exception("Sequence doesn't contain elements");
	}

	public T SingleOrDefault()
	{
		return default;
	}

	public T[] ToArray()
	{
		return Array.Empty<T>();
	}

	public T[] ToArray(out int length)
	{
		length = 0;
		return Array.Empty<T>();
	}

	public List<T> ToList()
	{
		return new List<T>();
	}

	public EmptyEnumerator<T> GetEnumerator()
	{
		return new EmptyEnumerator<T>();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		length = 0;
		return false;
	}
}