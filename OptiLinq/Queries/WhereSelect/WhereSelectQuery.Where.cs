using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public WhereQuery<TResult, TOtherWhereOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Where<TOtherWhereOperator>(TOtherWhereOperator @operator = default) where TOtherWhereOperator : struct, IFunction<TResult, bool>
	{
		return new WhereQuery<TResult, TOtherWhereOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, @operator);
	}

	public WhereQuery<TResult, FuncAsIFunction<TResult, bool>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> Where(Func<TResult, bool> @operator)
	{
		return new WhereQuery<TResult, FuncAsIFunction<TResult, bool>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(ref this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}