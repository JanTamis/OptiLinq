using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>
{
	public SelectManyQuery<T, TResult, TOperator, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}