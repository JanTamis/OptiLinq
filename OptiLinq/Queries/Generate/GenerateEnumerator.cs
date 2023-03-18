using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct GenerateEnumerator<T, TOperator> : IEnumerator<T> where TOperator : IFunction<T, T>
{
	private TOperator _operator;

	internal GenerateEnumerator(T current, TOperator @operator)
	{
		Current = current;
		_operator = @operator;
	}

	object IEnumerator.Current => Current;
	
	public T Current { get; private set; }

	public bool MoveNext()
	{
		Current = _operator.Eval(Current);
		return true;
	}

	public void Reset()
	{
	}

	public void Dispose()
	{

	}
}