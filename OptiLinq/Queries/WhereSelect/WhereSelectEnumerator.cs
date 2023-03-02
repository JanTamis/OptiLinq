using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> : IOptiEnumerator<TResult>
	where TWhereOperator : IFunction<T, bool>
	where TSelectOperator : IFunction<T, TResult>
	where TBaseEnumerator : struct, IOptiEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;

	private TWhereOperator _whereOperator;
	private TSelectOperator _selectOperator;

	public TResult Current { get; private set; } = default!;

	public WhereSelectEnumerator(TBaseEnumerator baseEnumerator, TWhereOperator whereOperator, TSelectOperator selectOperator)
	{
		_baseEnumerator = baseEnumerator;
		_whereOperator = whereOperator;
		_selectOperator = selectOperator;
	}

	public bool MoveNext()
	{
		while (_baseEnumerator.MoveNext())
		{
			if (_whereOperator.Eval(_baseEnumerator.Current))
			{
				Current = _selectOperator.Eval(_baseEnumerator.Current);
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