using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public SelectManyQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, IOptiQuery<TResult>> SelectMany<TOtherOperator, TResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TSubQuery> SelectMany<TOtherOperator, TSubQuery, TResult>(TOtherOperator @operator = default)
		where TOtherOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOtherOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}