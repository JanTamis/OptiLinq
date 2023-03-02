using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RangeQuery
{
	public SelectManyQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<int, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<int, TResult, FuncAsIFunction<int, IOptiQuery<TResult>>, RangeQuery, RangeEnumerator, IOptiQuery<TResult>> SelectMany<TResult>(Func<int, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<int, TResult, FuncAsIFunction<int, IOptiQuery<TResult>>, RangeQuery, RangeEnumerator, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<int, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<int, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<int, TResult, TOperator, RangeQuery, RangeEnumerator, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<int, TResult, FuncAsIFunction<int, TSubQuery>, RangeQuery, RangeEnumerator, TSubQuery> SelectMany<TSubQuery, TResult>(Func<int, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<int, TResult, FuncAsIFunction<int, TSubQuery>, RangeQuery, RangeEnumerator, TSubQuery>(ref this, new FuncAsIFunction<int, TSubQuery>(@operator));
	}
}