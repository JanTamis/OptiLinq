using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public SelectQuery<T, TResult, TSelectOperator, EnumerableQuery<T>, IEnumerator<T>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, EnumerableQuery<T>, IEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, EnumerableQuery<T>, IEnumerator<T>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, EnumerableQuery<T>, IEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, EnumerableQuery<T>, IEnumerator<T>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, EnumerableQuery<T>, IEnumerator<T>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}