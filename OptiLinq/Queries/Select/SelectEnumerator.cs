using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectEnumerator<T, TResult, TOperator, TBaseEnumerator> : IOptiEnumerator<TResult>
	where TOperator : IFunction<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private TOperator _operator;

	public TResult Current => _operator.Eval(_baseEnumerator.Current);

	public SelectEnumerator(TBaseEnumerator baseEnumerator, TOperator @operator)
	{
		_baseEnumerator = baseEnumerator;
		_operator = @operator;
	}

	public bool MoveNext()
	{
		return _baseEnumerator.MoveNext();
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}