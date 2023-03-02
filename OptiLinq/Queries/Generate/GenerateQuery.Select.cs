using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public SelectQuery<T, TResult, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}