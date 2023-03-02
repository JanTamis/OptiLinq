using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public WhereQuery<T, TWhereOperator, ListQuery<T>, ListEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, ListQuery<T>, ListEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, ListEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, ListQuery<T>, ListEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}