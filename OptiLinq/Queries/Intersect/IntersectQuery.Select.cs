using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public SelectQuery<T, TResult, TSelectOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}