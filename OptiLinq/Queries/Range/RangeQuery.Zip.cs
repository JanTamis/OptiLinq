using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RangeQuery
{
	public ZipQuery<int, TResult, TOperator, RangeQuery, TOtherQuery> Zip<TResult, TOperator, TOtherQuery>(in TOtherQuery other, TOperator @operator = default)
		where TOperator : struct, IFunction<int, int, TResult>
		where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ZipQuery<int, TResult, TOperator, RangeQuery, TOtherQuery>(@operator, in this, in other);
	}

	public ZipQuery<int, TResult, FuncAsIFunction<int, int, TResult>, RangeQuery, TOtherQuery> Zip<TResult, TOtherQuery>(in TOtherQuery other, Func<int, int, TResult> @operator)
		where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ZipQuery<int, TResult, FuncAsIFunction<int, int, TResult>, RangeQuery, TOtherQuery>(new FuncAsIFunction<int, int, TResult>(@operator), in this, in other);
	}
}