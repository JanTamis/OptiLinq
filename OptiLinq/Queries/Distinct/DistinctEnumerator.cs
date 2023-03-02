using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct DistinctEnumerator<T, TBaseEnumerator, TComparer> : IOptiEnumerator<T>
	where TBaseEnumerator : IOptiEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private TBaseEnumerator _baseEnumerator;
	private PooledSet<T, TComparer> _set;

	internal DistinctEnumerator(TBaseEnumerator baseEnumerator, TComparer comparer, int capacity)
	{
		_baseEnumerator = baseEnumerator;
		_set = new PooledSet<T, TComparer>(capacity, comparer);
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