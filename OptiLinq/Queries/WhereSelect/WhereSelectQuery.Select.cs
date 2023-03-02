using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<TResult, TOtherResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Select<TOtherSelectOperator, TOtherResult>(TOtherSelectOperator selector = default) where TOtherSelectOperator : struct, IFunction<TResult, TOtherResult>
	{
		return new SelectQuery<TResult, TOtherResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<TResult, TResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Select<TOtherSelectOperator>(TOtherSelectOperator selector = default) where TOtherSelectOperator : struct, IFunction<TResult, TResult>
	{
		return new SelectQuery<TResult, TResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Select<TOtherResult>(Func<TResult, TOtherResult> selector)
	{
		return new SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, new FuncAsIFunction<TResult, TOtherResult>(selector));
	}
}