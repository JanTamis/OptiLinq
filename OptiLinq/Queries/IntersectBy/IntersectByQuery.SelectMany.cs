using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public SelectManyQuery<T, TResult, TOperator, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}