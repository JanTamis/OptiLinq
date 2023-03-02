using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<T, TResult, TSelectOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}