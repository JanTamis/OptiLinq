using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RangeQuery
{
	public SelectQuery<int, TResult, TSelectOperator, RangeQuery, RangeEnumerator> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<int, TResult>
	{
		return new SelectQuery<int, TResult, TSelectOperator, RangeQuery, RangeEnumerator>(ref this, selector);
	}

	public SelectQuery<int, int, TSelectOperator, RangeQuery, RangeEnumerator> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<int, int>
	{
		return new SelectQuery<int, int, TSelectOperator, RangeQuery, RangeEnumerator>(ref this, selector);
	}

	public SelectQuery<int, TResult, FuncAsIFunction<int, TResult>, RangeQuery, RangeEnumerator> Select<TResult>(Func<int, TResult> selector)
	{
		return new SelectQuery<int, TResult, FuncAsIFunction<int, TResult>, RangeQuery, RangeEnumerator>(ref this, new FuncAsIFunction<int, TResult>(selector));
	}
}