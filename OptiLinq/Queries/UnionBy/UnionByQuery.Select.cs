using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>
{
	public SelectQuery<T, TResult, TSelectOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}