using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public ZipQuery<TResult, TOtherResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TOtherQuery> Zip<TOtherResult, TOtherOperator, TOtherQuery>(in TOtherQuery other, TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TResult, TOtherResult>
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TOtherQuery> Zip<TOtherResult, TOtherQuery>(in TOtherQuery other, Func<TResult, TResult, TOtherResult> @operator)
		where TOtherQuery : struct, IOptiQuery<TResult>
	{
		return new ZipQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TResult, TOtherResult>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>, TOtherQuery>(new FuncAsIFunction<TResult, TResult, TOtherResult>(@operator), in this, in other);
	}
}