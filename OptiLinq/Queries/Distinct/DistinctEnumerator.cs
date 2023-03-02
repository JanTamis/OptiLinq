using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct DistinctEnumerator<T, TBaseEnumerator> : IOptiEnumerator<T> where TBaseEnumerator : IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private PooledSet<T, IEqualityComparer<T>> _set;

	internal DistinctEnumerator(TBaseEnumerator baseEnumerator, IEqualityComparer<T> comparer, int capacity)
	{
		_baseEnumerator = baseEnumerator;
		_set = new PooledSet<T, IEqualityComparer<T>>(capacity, comparer);
	}

	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (_set.AddIfNotPresent(_baseEnumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public void Dispose()
	{
		_set.Dispose();
		_baseEnumerator.Dispose();
	}
}