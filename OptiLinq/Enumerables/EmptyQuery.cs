using OptiLinq.Interfaces;

namespace OptiLinq;

public struct EmptyQuery<T> : IOptiQuery<T, EmptyEnumerator<T>>
{
	public bool Any()
	{
		return false;
	}

	public bool Contains(T item, IEqualityComparer<T>? comparer)
	{
		return false;
	}

	public int Count()
	{
		return 0;
	}

	public T First()
	{
		return default;
	}

	public T FirstOrDefault()
	{
		return default;
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