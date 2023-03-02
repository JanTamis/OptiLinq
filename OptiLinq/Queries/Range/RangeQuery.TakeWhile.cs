using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RangeQuery
{
	public TakeWhileQuery<int, TOperator, RangeQuery, RangeEnumerator> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<int, bool>
	{
		return new TakeWhileQuery<int, TOperator, RangeQuery, RangeEnumerator>(this, @operator);
	}

	public TakeWhileQuery<int, FuncAsIFunction<int, bool>, RangeQuery, RangeEnumerator> TakeWhile(Func<int, bool> @operator)
	{
		return new TakeWhileQuery<int, FuncAsIFunction<int, bool>, RangeQuery, RangeEnumerator>(this, new FuncAsIFunction<int, bool>(@operator));
	}
}