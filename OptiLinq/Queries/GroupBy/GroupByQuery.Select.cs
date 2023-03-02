using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SelectQuery<ArrayQuery<T>, TResult, TSelectOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<ArrayQuery<T>, TResult>
	{
		return new SelectQuery<ArrayQuery<T>, TResult, TSelectOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, selector);
	}

	public SelectQuery<ArrayQuery<T>, ArrayQuery<T>, TSelectOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<ArrayQuery<T>, ArrayQuery<T>>
	{
		return new SelectQuery<ArrayQuery<T>, ArrayQuery<T>, TSelectOperator, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, selector);
	}

	public SelectQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, TResult>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Select<TResult>(Func<ArrayQuery<T>, TResult> selector)
	{
		return new SelectQuery<ArrayQuery<T>, TResult, FuncAsIFunction<ArrayQuery<T>, TResult>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, new FuncAsIFunction<ArrayQuery<T>, TResult>(selector));
	}
}