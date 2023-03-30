using System.Collections;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct IntersectByEnumerator<T, TKey, TEnumerator, TComparer, TKeySelector> : IEnumerator<T>
	where TComparer : IEqualityComparer<TKey>
	where TKeySelector : IFunction<T, TKey>
	where TEnumerator : IEnumerator<T>
{
	private TEnumerator _enumerator;
	private PooledSet<TKey, TComparer> _set;
	private TKeySelector _keySelector;

	public IntersectByEnumerator(TEnumerator enumerator, PooledSet<TKey, TComparer> set, TKeySelector keySelector)
	{
		_enumerator = enumerator;
		_set = set;
		_keySelector = keySelector;
	}

	object IEnumerator.Current => Current;

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		while (_enumerator.MoveNext())
		{
			if (_set.Remove(_keySelector.Eval(_enumerator.Current)))
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