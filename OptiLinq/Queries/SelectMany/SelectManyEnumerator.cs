using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator> : IEnumerator<TResult>
	where TOperator : struct, IFunction<T, TSubQuery>
	where TBaseEnumerator : IEnumerator<T>
	where TSubQuery : IOptiQuery<TResult>
{
	private TBaseEnumerator _baseEnumerator;
	private IEnumerator<TResult>? _subEnumerator;

	private TOperator _selector;

	private int _state;

	internal SelectManyEnumerator(TBaseEnumerator baseEnumerator, TOperator selector)
	{
		_baseEnumerator = baseEnumerator;
		_selector = selector;
		_subEnumerator = default;
		_state = 0;
	}


	object IEnumerator.Current => Current;

	public TResult Current => _subEnumerator.Current;

	public bool MoveNext()
	{
		switch (_state)
		{
			case 0:
			{
				if (_baseEnumerator.MoveNext())
				{
					_subEnumerator = _selector.Eval(_baseEnumerator.Current).GetEnumerator();
					_state = 1;
				}
				else
				{
					_subEnumerator?.Dispose();
					return false;
				}
			}
				goto case 1;
			case 1:
			{
				if (_subEnumerator!.MoveNext())
				{
					return true;
				}

				_subEnumerator.Dispose();
				_state = 0;
			}
				goto case 0;
		}

		return false;
	}

	public void Reset()
	{
		_state = 0;
		_baseEnumerator.Reset();
		_subEnumerator?.Dispose();
	}

	public void Dispose()
	{
		_baseEnumerator.Dispose();
		_subEnumerator?.Dispose();
	}
}