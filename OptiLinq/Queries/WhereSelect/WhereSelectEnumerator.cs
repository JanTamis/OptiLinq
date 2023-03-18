using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator> : IEnumerator<TResult>
	where TWhereOperator : IFunction<T, bool>
	where TSelectOperator : IFunction<T, TResult>
	where TBaseEnumerator : IEnumerator<T>
{
	private TBaseEnumerator _baseEnumerator;

	private TWhereOperator _whereOperator;
	private TSelectOperator _selectOperator;


	object IEnumerator.Current => Current;
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

	public void Reset()
	{
		_baseEnumerator.Reset();
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
	}
}