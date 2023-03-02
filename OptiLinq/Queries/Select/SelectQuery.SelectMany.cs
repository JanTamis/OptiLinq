using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public SelectManyQuery<TResult, TOtherResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>> SelectMany<TSelectOperator, TOtherResult>(TSelectOperator @operator = default)
		where TSelectOperator : struct, IFunction<TResult, IOptiQuery<TOtherResult>>
	{
		return new SelectManyQuery<TResult, TOtherResult, TSelectOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>> SelectMany<TOtherResult>(Func<TResult, IOptiQuery<TOtherResult>> @operator)
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, IOptiQuery<TOtherResult>>(ref this, new FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>(@operator));
	}

	public SelectManyQuery<TResult, TOtherResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TSubQuery> SelectMany<TOtherOperator, TSubQuery, TOtherResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, TOtherOperator, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TSubQuery> SelectMany<TSubQuery, TOtherResult>(Func<TResult, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TSubQuery>, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>, TSubQuery>(ref this, new FuncAsIFunction<TResult, TSubQuery>(@operator));
	}
}