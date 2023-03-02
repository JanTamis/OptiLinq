using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RandomQuery
{
	public WhereQuery<int, TWhereOperator, RandomQuery, RandomEnumerator> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<int, bool>
	{
		return new WhereQuery<int, TWhereOperator, RandomQuery, RandomEnumerator>(ref this, @operator);
	}

	public WhereQuery<int, FuncAsIFunction<int, bool>, RandomQuery, RandomEnumerator> Where(Func<int, bool> @operator)
	{
		return new WhereQuery<int, FuncAsIFunction<int, bool>, RandomQuery, RandomEnumerator>(ref this, new FuncAsIFunction<int, bool>(@operator));
	}
}