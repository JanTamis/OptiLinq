using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public SelectQuery<T, TResult, TSelectOperator, ArrayQuery<T>, ArrayEnumerator<T>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, ArrayQuery<T>, ArrayEnumerator<T>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ArrayQuery<T>, ArrayEnumerator<T>> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}