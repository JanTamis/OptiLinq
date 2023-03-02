using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<TResult> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SingletonQuery<TResult>(selector.Eval(in _element));
	}

	public SingletonQuery<T> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SingletonQuery<T>(selector.Eval(in _element));
	}

	public SingletonQuery<TResult> Select<TResult>(Func<T, TResult> selector)
	{
		return new SingletonQuery<TResult>(selector(_element));
	}
}