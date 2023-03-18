using System.Collections;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct UnionEnumerator<T, TEnumerator, TComparer> : IEnumerator<T>
	where TComparer : IEqualityComparer<T>
	where TEnumerator : IEnumerator<T>
{
	private TEnumerator _firstEnumerator;
	private PooledSet<T, TComparer> _set;

	internal UnionEnumerator(TEnumerator firstEnumerator, PooledSet<T, TComparer> set)
	{
		_firstEnumerator = firstEnumerator;
		_set = set;
	}

	object IEnumerator.Current { get; }
	public T Current { get; private set; }

	public bool MoveNext()
	{
		while (_firstEnumerator.MoveNext())
		{
			if (_set.Add(_firstEnumerator.Current))
			{
				Current = _firstEnumerator.Current;
				return true;
			}
		}

		Current = default!;
		return false;
	}

	public void Reset()
	{
		_firstEnumerator.Reset();
		_set.Clear();
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_set.Dispose();
	}
}