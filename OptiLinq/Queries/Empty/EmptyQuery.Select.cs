using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<TResult> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new EmptyQuery<TResult>();
	}

	public EmptyQuery<T> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return this;
	}

	public EmptyQuery<TResult> Select<TResult>(Func<T, TResult> selector)
	{
		return new EmptyQuery<TResult>();
	}
}