using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct IntersectEnumerator<T, TFirstEnumerator, TComparer> : IEnumerator<T>
	where TComparer : IEqualityComparer<T>
	where TFirstEnumerator : IEnumerator<T>
{
	private TFirstEnumerator _firstEnumerator;
	private PooledSet<T, TComparer> _set;

	public IntersectEnumerator(TFirstEnumerator firstEnumerator, PooledSet<T, TComparer> set)
	{
		_firstEnumerator = firstEnumerator;
		_set = set;
	}

	object IEnumerator.Current => Current;
	
	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		while (_firstEnumerator.MoveNext())
		{
			if (_set.Remove(_firstEnumerator.Current))
			{
				Current = _firstEnumerator.Current;
				return true;
			}
		}

		return false;
	}

	public void Reset()
	{
		throw new NotSupportedException();
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_set.Dispose();
	}
}