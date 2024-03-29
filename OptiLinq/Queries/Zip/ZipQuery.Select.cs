using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public SelectQuery<TResult, TOtherResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>> Select<TSelectOperator, TOtherResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TOtherResult>
	{
		return new SelectQuery<TResult, TOtherResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(ref this, selector);
	}

	public SelectQuery<TResult, TResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TResult>
	{
		return new SelectQuery<TResult, TResult, TSelectOperator, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(ref this, selector);
	}

	public SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>> Select<TOtherResult>(Func<TResult, TOtherResult> selector)
	{
		return new SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>>(ref this, new FuncAsIFunction<TResult, TOtherResult>(selector));
	}
}