using System.Collections;
using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct UnionByEnumerator<T, TKey, TFirstEnumerator, TSecondEnumerator, TKeySelector, TComparer> : IEnumerator<T>
	where TFirstEnumerator : IEnumerator<T>
	where TSecondEnumerator : IEnumerator<T>
	where TKeySelector : IFunction<T, TKey>
	where TComparer : IEqualityComparer<TKey>
{
	private TFirstEnumerator _firstEnumerator;
	private TSecondEnumerator _secondEnumerator;
	private TKeySelector _keySelector;
	private PooledSet<TKey, TComparer> _set;

	internal UnionByEnumerator(TFirstEnumerator firstEnumerator, TSecondEnumerator secondEnumerator, TKeySelector selector, PooledSet<TKey, TComparer> set)
	{
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
		_keySelector = selector;
		_set = set;
	}

	object IEnumerator.Current => Current;
	public T Current { get; private set; }

	public bool MoveNext()
	{
		while (_firstEnumerator.MoveNext())
		{
			if (_set.Add(_keySelector.Eval(_firstEnumerator.Current)))
			{
				Current = _firstEnumerator.Current;
				return true;
			}
		}

		while (_secondEnumerator.MoveNext())
		{
			if (_set.Add(_keySelector.Eval(_secondEnumerator.Current)))
			{
				Current = _secondEnumerator.Current;
				return true;
			}
		}

		Current = default!;
		return false;
	}

	public void Reset()
	{
		_firstEnumerator.Reset();
		_secondEnumerator.Reset();

		_set.Clear();
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_secondEnumerator.Dispose();

		_set.Dispose();
	}
}