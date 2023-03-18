using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public WhereQuery<T, TWhereOperator, HashSetQuery<T>, HashSet<T>.Enumerator> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSet<T>.Enumerator> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}