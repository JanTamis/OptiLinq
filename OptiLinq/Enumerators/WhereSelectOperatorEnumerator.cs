using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereSelectOperatorEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> : IOptiEnumerator<TResult>
	where TWhereOperator : IFunction<T, bool>
	where TSelectOperator : IFunction<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;

	private TResult _current;

	public TResult Current => _current;

	public WhereSelectOperatorEnumerator(TBaseEnumerator baseEnumerator)
	{
		_baseEnumerator = baseEnumerator;
	}

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (TWhereOperator.Eval(_baseEnumerator.Current))
			{
				_current = TSelectOperator.Eval(_baseEnumerator.Current);
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