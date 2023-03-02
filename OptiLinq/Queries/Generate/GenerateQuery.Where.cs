using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public WhereQuery<T, TWhereOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}