using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ConcatQuery<T> : IOptiQuery<T, ConcatEnumerator<T>>
{
	public bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>
	{
		throw new NotImplementedException();
	}

	public bool Any()
	{
		throw new NotImplementedException();
	}

	public IEnumerable<T> AsEnumerable()
	{
		throw new NotImplementedException();
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer)
	{
		throw new NotImplementedException();
	}

	public int Count()
	{
		throw new NotImplementedException();
	}

	public T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		throw new NotImplementedException();
	}

	public T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>
	{
		throw new NotImplementedException();
	}

	public T First()
	{
		throw new NotImplementedException();
	}

	public T FirstOrDefault()
	{
		throw new NotImplementedException();
	}

	public T Last()
	{
		throw new NotImplementedException();
	}

	public T LastOrDefault()
	{
		throw new NotImplementedException();
	}

	public T Max()
	{
		throw new NotImplementedException();
	}

	public T Min()
	{
		throw new NotImplementedException();
	}

	public T Single()
	{
		throw new NotImplementedException();
	}

	public T SingleOrDefault()
	{
		throw new NotImplementedException();
	}

	public T[] ToArray()
	{
		throw new NotImplementedException();
	}

	public T[] ToArray(out int length)
	{
		throw new NotImplementedException();
	}

	public List<T> ToList()
	{
		throw new NotImplementedException();
	}

	public bool TryGetNonEnumeratedCount(out int length)
	{
		throw new NotImplementedException();
	}

	public ConcatEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	
}