using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public SelectQuery<T, TResult, TSelectOperator, HashSetQuery<T>, HashSet<T>.Enumerator> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, TResult>
	{
		return new SelectQuery<T, TResult, TSelectOperator, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, selector);
	}

	public SelectQuery<T, T, TSelectOperator, HashSetQuery<T>, HashSet<T>.Enumerator> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T, T>
	{
		return new SelectQuery<T, T, TSelectOperator, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, selector);
	}

	public SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, HashSetQuery<T>, HashSet<T>.Enumerator> Select<TResult>(Func<T, TResult> selector)
	{
		return new SelectQuery<T, TResult, FuncAsIFunction<T, TResult>, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, new FuncAsIFunction<T, TResult>(selector));
	}
}