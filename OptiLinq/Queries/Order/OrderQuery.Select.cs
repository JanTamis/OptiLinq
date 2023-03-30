using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SelectQuery<T, TResult, TSelectOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}