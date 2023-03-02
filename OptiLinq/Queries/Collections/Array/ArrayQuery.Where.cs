using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public WhereQuery<T, TWhereOperator, ArrayQuery<T>, ArrayEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, ArrayQuery<T>, ArrayEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}