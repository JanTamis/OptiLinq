using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SelectManyQuery<T, TResult, TOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}