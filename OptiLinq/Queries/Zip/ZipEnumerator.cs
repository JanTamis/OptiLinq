using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ZipEnumerator<T, TResult, TOperator> : IOptiEnumerator<TResult>
	where TOperator : struct, IFunction<T, T, TResult>
{
	private TOperator _operator;
	private readonly IOptiEnumerator<T> _firstEnumerator;
	private readonly IOptiEnumerator<T> _secondEnumerator;

	internal ZipEnumerator(in TOperator @operator, IOptiEnumerator<T> firstEnumerator, IOptiEnumerator<T> secondEnumerator)
	{
		_operator = @operator;
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
	}

	public TResult Current => _operator.Eval(_firstEnumerator.Current, _secondEnumerator.Current);

	public bool MoveNext()
	{
		return _firstEnumerator.MoveNext() && _secondEnumerator.MoveNext();
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_secondEnumerator.Dispose();
	}
}