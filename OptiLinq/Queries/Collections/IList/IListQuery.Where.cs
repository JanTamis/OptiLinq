using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IListQuery<T>
{
	public WhereQuery<T, TWhereOperator, IListQuery<T>, IListEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, IListQuery<T>, IListEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, IListQuery<T>, IListEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, IListQuery<T>, IListEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}