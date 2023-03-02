using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<TResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> TakeWhile<TOtherOperator>(TOtherOperator @operator)
		where TOtherOperator : struct, IFunction<TResult, bool>
	{
		return new TakeWhileQuery<TResult, TOtherOperator, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>> TakeWhile(Func<TResult, bool> @operator)
	{
		return new TakeWhileQuery<TResult, FuncAsIFunction<TResult, bool>, WhereSelectQuery<T, TResult, TWhereOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>, WhereSelectEnumerator<T, TResult, TWhereOperator, TSelectOperator, TBaseEnumerator>>(this, new FuncAsIFunction<TResult, bool>(@operator));
	}
}