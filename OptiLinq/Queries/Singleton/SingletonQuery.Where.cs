using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public WhereQuery<T, TWhereOperator, SingletonQuery<T>, SingletonEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, SingletonQuery<T>, SingletonEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}