using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectOperatorEnumerator<T, TResult, TOperator, TBaseEnumerator> : IOptiEnumerator<TResult>
	where TOperator : ISelectOperator<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;
	private TResult _current;

	public TResult Current => _current;

	public SelectOperatorEnumerator(TBaseEnumerator baseEnumerator)
	{
		_baseEnumerator = baseEnumerator;
	}

	public bool MoveNext()
	{
		if (_baseEnumerator.MoveNext())
		{
			_current = TOperator.Transform(_baseEnumerator.Current);

			return true;
		}

		return false;
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}