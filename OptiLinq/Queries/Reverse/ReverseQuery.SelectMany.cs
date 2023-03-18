using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SelectManyQuery<T, TResult, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}