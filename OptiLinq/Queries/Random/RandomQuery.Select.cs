using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RandomQuery
{
	public SelectQuery<int, TResult, TSelectOperator, RandomQuery, RandomEnumerator> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<int, TResult>
	{
		return new SelectQuery<int, TResult, TSelectOperator, RandomQuery, RandomEnumerator>(ref this, selector);
	}

	public SelectQuery<int, int, TSelectOperator, RandomQuery, RandomEnumerator> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<int, int>
	{
		return new SelectQuery<int, int, TSelectOperator, RandomQuery, RandomEnumerator>(ref this, selector);
	}

	public SelectQuery<int, TResult, FuncAsIFunction<int, TResult>, RandomQuery, RandomEnumerator> Select<TResult>(Func<int, TResult> selector)
	{
		return new SelectQuery<int, TResult, FuncAsIFunction<int, TResult>, RandomQuery, RandomEnumerator>(ref this, new FuncAsIFunction<int, TResult>(selector));
	}
}