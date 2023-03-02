using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SelectManyQuery<T, TResult, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, IOptiQuery<TResult>> SelectMany<TOperator, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, IOptiQuery<TResult>>
	{
		return new SelectManyQuery<T, TResult, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, IOptiQuery<TResult>>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, IOptiQuery<TResult>> SelectMany<TResult>(Func<T, IOptiQuery<TResult>> @operator)
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, IOptiQuery<TResult>>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, IOptiQuery<TResult>>(ref this, new FuncAsIFunction<T, IOptiQuery<TResult>>(@operator));
	}

	public SelectManyQuery<T, TResult, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, TSubQuery> SelectMany<TOperator, TSubQuery, TResult>(TOperator @operator = default)
		where TOperator : struct, IFunction<T, TSubQuery>
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, TOperator, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, TSubQuery>(ref this, @operator);
	}

	public SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, TSubQuery> SelectMany<TSubQuery, TResult>(Func<T, TSubQuery> @operator)
		where TSubQuery : struct, IOptiQuery<TResult>
	{
		return new SelectManyQuery<T, TResult, FuncAsIFunction<T, TSubQuery>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>, TSubQuery>(ref this, new FuncAsIFunction<T, TSubQuery>(@operator));
	}
}