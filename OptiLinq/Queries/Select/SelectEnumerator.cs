using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectEnumerator<T, TResult, TOperator, TBaseEnumerator> : IEnumerator<TResult>
	where TOperator : IFunction<T, TResult>
	where TBaseEnumerator : IEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private TOperator _operator;


	object IEnumerator.Current => Current;

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

	public void Reset()
	{
		_baseEnumerator.Reset();
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}