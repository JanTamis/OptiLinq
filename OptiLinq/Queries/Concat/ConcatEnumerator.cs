using System.Collections;

namespace OptiLinq;

public struct ConcatEnumerator<T, TFirstEnumerator, TSecondEnumerator> : IEnumerator<T>
	where TFirstEnumerator : IEnumerator<T>
	where TSecondEnumerator : IEnumerator<T>
{
	private TFirstEnumerator _firstEnumerator;
	private TSecondEnumerator _secondEnumerator;

	public ConcatEnumerator(TFirstEnumerator firstEnumerator, TSecondEnumerator secondEnumerator)
	{
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
	}

	object IEnumerator.Current => Current;
	
	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		if (_firstEnumerator.MoveNext())
		{
			Current = _firstEnumerator.Current;
			return true;
		}

		if (_secondEnumerator.MoveNext())
		{
			Current = _secondEnumerator.Current;
			return true;
		}

		return false;
	}

	public void Reset()
	{
		_firstEnumerator.Reset();
		_secondEnumerator.Reset();
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_secondEnumerator.Dispose();
	}
}