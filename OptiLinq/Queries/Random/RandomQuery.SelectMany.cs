using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RandomQuery
{
	public SelectManyQuery<int, TResult, TOperator, RandomQuery, RandomEnumerator, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<int, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<int, TResult, TOperator, RandomQuery, RandomEnumerator, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<int, TResult, FuncAsIFunction<int, IOptiQuery<TResult>>, RandomQuery, RandomEnumerator, IOptiQuery<TResult>> SelectMany<TResult>(Func<int, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<int, TResult, FuncAsIFunction<int, IOptiQuery<TResult>>, RandomQuery, RandomEnumerator, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<int, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<int, TResult, TOperator, RandomQuery, RandomEnumerator, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<int, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<int, TResult, TOperator, RandomQuery, RandomEnumerator, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<int, TResult, FuncAsIFunction<int, TSubQuery>, RandomQuery, RandomEnumerator, TSubQuery> SelectMany<TSubQuery, TResult>(Func<int, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<int, TResult, FuncAsIFunction<int, TSubQuery>, RandomQuery, RandomEnumerator, TSubQuery>(ref this, new FuncAsIFunction<int, TSubQuery>(@operator));
	}
}