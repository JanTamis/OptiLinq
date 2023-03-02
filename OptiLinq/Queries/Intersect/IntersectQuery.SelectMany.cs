using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public SelectManyQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}