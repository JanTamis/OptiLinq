using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RangeQuery
{
	public WhereQuery<int, TWhereOperator, RangeQuery, RangeEnumerator> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<int, bool>
	{
		return new WhereQuery<int, TWhereOperator, RangeQuery, RangeEnumerator>(ref this, @operator);
	}

	public WhereQuery<int, FuncAsIFunction<int, bool>, RangeQuery, RangeEnumerator> Where(Func<int, bool> @operator)
	{
		return new WhereQuery<int, FuncAsIFunction<int, bool>, RangeQuery, RangeEnumerator>(ref this, new FuncAsIFunction<int, bool>(@operator));
	}
}