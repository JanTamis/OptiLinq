using OptiLinq.Interfaces;

namespace OptiLinq;

public struct TakeWhileEnumerator<T, TOperator, TBaseEnumerator> : IOptiEnumerator<T>
	where TOperator : struct, IFunction<T, bool>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private TOperator _operator;

	public TakeWhileEnumerator(TBaseEnumerator baseEnumerator, TOperator @operator)
	{
		_baseEnumerator = baseEnumerator;
		_operator = @operator;
	}

	public T Current => _baseEnumerator.Current;

	public bool MoveNext()
	{
		return _baseEnumerator.MoveNext() && _operator.Eval(_baseEnumerator.Current);
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}