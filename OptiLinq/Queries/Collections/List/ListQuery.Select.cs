using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public SelectQuery<T, TResult, TSelectOperator, ListQuery<T>, List<T>.Enumerator> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, ListQuery<T>, List<T>.Enumerator>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, ListQuery<T>, List<T>.Enumerator> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, ListQuery<T>, List<T>.Enumerator>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ListQuery<T>, List<T>.Enumerator> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ListQuery<T>, List<T>.Enumerator>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}