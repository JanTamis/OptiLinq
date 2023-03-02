using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator>
{
	public WhereQuery<T, TWhereOperator, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}