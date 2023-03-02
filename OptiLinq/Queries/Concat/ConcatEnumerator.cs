using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ConcatEnumerator<T> : IOptiEnumerator<T>
{
	private readonly IOptiEnumerator<T> _firstEnumerator;
	private readonly IOptiEnumerator<T> _secondEnumerator;

	public ConcatEnumerator(IOptiEnumerator<T> firstEnumerator, IOptiEnumerator<T> secondEnumerator)
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