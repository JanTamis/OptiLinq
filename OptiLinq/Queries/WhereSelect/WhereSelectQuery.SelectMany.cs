using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public SelectManyQuery<TResult, TOtherResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>> SelectMany<TOtherSelectOperator, TOtherResult>(TOtherSelectOperator @operator = default)
		where TOtherSelectOperator : struct, IFunction<TResult, IOptiQuery<TOtherResult>>
	{
		return new SelectManyQuery<TResult, TOtherResult, TOtherSelectOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>> SelectMany<TOtherResult>(Func<TResult, IOptiQuery<TOtherResult>> @operator)
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>>(ref this, new FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>(@operator));
	}

	public SelectManyQuery<TResult, TOtherResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TSubQuery> SelectMany<TOtherOperator, TSubQuery, TOtherResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TSubQuery> SelectMany<TSubQuery, TOtherResult>(Func<TResult, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TSubQuery>(ref this, new FuncAsIFunction<TResult, TSubQuery>(@operator));
	}
}