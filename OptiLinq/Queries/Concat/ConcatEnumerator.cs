using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ConcatEnumerator<T, TFirstEnumerator, TSecondEnumerator> : IOptiEnumerator<T>
	where TFirstEnumerator : IOptiEnumerator<T>
	where TSecondEnumerator : IOptiEnumerator<T>
{
	private TFirstEnumerator _firstEnumerator;
	private TSecondEnumerator _secondEnumerator;

	public ConcatEnumerator(TFirstEnumerator firstEnumerator, TSecondEnumerator secondEnumerator)
	{
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
	}

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

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_secondEnumerator.Dispose();
	}
}