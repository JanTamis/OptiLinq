using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<TResult, TOtherResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> Select<TSelectOperator, TOtherResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TOtherResult>
	{
		return new SelectQuery<TResult, TOtherResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<TResult, TResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TResult>
	{
		return new SelectQuery<TResult, TResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> Select<TOtherResult>(Func<TResult, TOtherResult> selector)
	{
		return new SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(ref this, new FuncAsIFunction<TResult, TOtherResult>(selector));
	}
}