using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct DistinctEnumerator<T, TBaseEnumerator, TComparer> : IEnumerator<T>
	where TBaseEnumerator : IEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private TBaseEnumerator _baseEnumerator;
	private PooledSet<T, TComparer> _set;

	internal DistinctEnumerator(TBaseEnumerator baseEnumerator, TComparer comparer, int capacity)
	{
		_baseEnumerator = baseEnumerator;
		_set = new PooledSet<T, TComparer>(capacity, comparer);
	}

	object IEnumerator.Current => Current;

	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (_set.Add(_baseEnumerator.Current))
			{
				return true;
			}
		}

		return false;
	}

	public void Reset()
	{
		_set.Clear();
		_baseEnumerator.Reset();
	}

	public void Dispose()
	{
		_set.Dispose();
		_baseEnumerator.Dispose();
	}
}