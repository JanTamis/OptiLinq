using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public SelectManyQuery<TResult, TOtherResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, IOptiQuery<TOtherResult>> SelectMany<TSelectOperator, TOtherResult>(TSelectOperator @operator = default)
		where TSelectOperator : struct, IFunction<TResult, IOptiQuery<TOtherResult>>
	{
		return new SelectManyQuery<TResult, TOtherResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, IOptiQuery<TOtherResult>>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, IOptiQuery<TOtherResult>> SelectMany<TOtherResult>(Func<TResult, IOptiQuery<TOtherResult>> @operator)
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, IOptiQuery<TOtherResult>>(ref this, new FuncAsIFunction<TResult, IOptiQuery<TOtherResult>>(@operator));
	}

	public SelectManyQuery<TResult, TOtherResult, TOtherOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherSubQuery> SelectMany<TOtherOperator, TOtherSubQuery, TOtherResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<TResult, TOtherSubQuery>
		where TOtherSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, TOtherOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherSubQuery>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherSubQuery> SelectMany<TOtherSubQuery, TOtherResult>(Func<TResult, TOtherSubQuery> @operator)
		where TOtherSubQuery : struct, IOptiQuery<TOtherResult>
	{
		return new SelectManyQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherSubQuery>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>, TOtherSubQuery>(ref this, new FuncAsIFunction<TResult, TOtherSubQuery>(@operator));
	}
}