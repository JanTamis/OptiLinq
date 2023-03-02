using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public SelectQuery<T, TResult, TSelectOperator, HashSetQuery<T>, HashSetEnumerator<T>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, HashSetQuery<T>, HashSetEnumerator<T>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, HashSetQuery<T>, HashSetEnumerator<T>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}