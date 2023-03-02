using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new WhereSelectQuery<T, TResult, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, _operator, selector);
	}

	public WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new WhereSelectQuery<T, T, TOperator, TSelectOperator, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, _operator, selector);
	}

	public WhereSelectQuery<T, TResult, TOperator, FuncAsIFunction<T, TResult>, TBaseQuery, TBaseEnumerator> Select<TResult>(Func<T, TResult> selector)
	{
		return new WhereSelectQuery<T, TResult, TOperator, FuncAsIFunction<T, TResult>, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, _operator, new FuncAsIFunction<T, TResult>(selector));
	}
}