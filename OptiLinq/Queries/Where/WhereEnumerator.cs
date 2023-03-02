using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereEnumerator<T, TOperator, TBaseEnumerator> : IOptiEnumerator<T>
	where TOperator : IFunction<T, bool>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;

	private TOperator _operator;

	public T Current => _baseEnumerator.Current;

	public WhereEnumerator(TBaseEnumerator baseEnumerator, TOperator @operator)
	{
		_baseEnumerator = baseEnumerator;
		_operator = @operator;
	}

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (_operator.Eval(Current))
			{
				return true;
			}
		}

		return false;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}