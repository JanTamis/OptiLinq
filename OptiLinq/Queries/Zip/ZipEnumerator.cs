using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, TSecondEnumerator> : IEnumerator<TResult>
	where TOperator : struct, IFunction<T, T, TResult>
	where TFirstEnumerator : IEnumerator<T>
	where TSecondEnumerator : IEnumerator<T>
{
	private TOperator _operator;
	private TFirstEnumerator _firstEnumerator;
	private TSecondEnumerator _secondEnumerator;

	private TResult _current = default!;

	internal ZipEnumerator(in TOperator @operator, TFirstEnumerator firstEnumerator, TSecondEnumerator secondEnumerator)
	{
		_operator = @operator;
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
	}

	object IEnumerator.Current => Current;

	public TResult Current => _current;

	public bool MoveNext()
	{
		if (_firstEnumerator.MoveNext() && _secondEnumerator.MoveNext())
		{
			_current = _operator.Eval(_firstEnumerator.Current, _secondEnumerator.Current);
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