using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public WhereQuery<T, TWhereOperator, RepeatQuery<T>, RepeatEnumerator<T>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, RepeatQuery<T>, RepeatEnumerator<T>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}