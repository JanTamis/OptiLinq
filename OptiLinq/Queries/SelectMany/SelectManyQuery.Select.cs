using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>
{
	public SelectQuery<TResult, TOtherResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>> Select<TSelectOperator, TOtherResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TOtherResult>
	{
		return new SelectQuery<TResult, TOtherResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(ref this, selector);
	}

	public SelectQuery<TResult, TResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<TResult, TResult>
	{
		return new SelectQuery<TResult, TResult, TSelectOperator, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(ref this, selector);
	}

	public SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>> Select<TOtherResult>(Func<TResult, TOtherResult> selector)
	{
		return new SelectQuery<TResult, TOtherResult, FuncAsIFunction<TResult, TOtherResult>, SelectManyQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator, TSubQuery>, SelectManyEnumerator<T, TResult, TBaseEnumerator, TSubQuery, TOperator>>(ref this, new FuncAsIFunction<TResult, TOtherResult>(selector));
	}
}