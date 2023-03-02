using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
{
	public WhereQuery<T, TWhereOperator, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T, bool>
	{
		return new WhereQuery<T, TWhereOperator, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>>(ref this, @operator);
	}

	public WhereQuery<T, FuncAsIFunction<T, bool>, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>> Where(Func<T, bool> @operator)
	{
		return new WhereQuery<T, FuncAsIFunction<T, bool>, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T, bool>(@operator));
	}
}