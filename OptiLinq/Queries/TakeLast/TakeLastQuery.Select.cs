using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<T, TResult, TSelectOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}