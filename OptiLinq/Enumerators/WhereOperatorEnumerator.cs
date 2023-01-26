using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereOperatorEnumerator<T, TOperator, TBaseEnumerator> : IOptiEnumerator<T>
	where TOperator : IFunction<T, bool>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;

	public T Current => _baseEnumerator.Current;

	public WhereOperatorEnumerator(TBaseEnumerator baseEnumerator)
	{
		_baseEnumerator = baseEnumerator;
	}

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (TOperator.Eval(Current))
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