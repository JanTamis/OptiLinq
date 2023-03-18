using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public WhereQuery<T, TWhereOperator, EnumerableQuery<T>, IEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, EnumerableQuery<T>, IEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, IEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, EnumerableQuery<T>, IEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}