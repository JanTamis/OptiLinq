using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<T, TResult, TSelectOperator, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}