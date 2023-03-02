using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public SelectQuery<T, TResult, TSelectOperator, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}