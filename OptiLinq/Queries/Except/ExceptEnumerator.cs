using System.Collections;
using OptiLinq.Collections;

namespace OptiLinq;

public struct ExceptEnumerator<T, TEnumerator, TComparer> : IEnumerator<T>
	where TEnumerator : IEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private TEnumerator _enumerator;

	private PooledSet<T, TComparer> _set;

	public ExceptEnumerator(TEnumerator enumerator, PooledSet<T, TComparer> set)
	{
		_enumerator = enumerator;
		_set = set;
	}

	object IEnumerator.Current => Current;
	
	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		while (_enumerator.MoveNext())
		{
			if (_set.Add(_enumerator.Current))
			{
				Current = _enumerator.Current;
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
		_enumerator.Dispose();
		_set.Dispose();
	}
}