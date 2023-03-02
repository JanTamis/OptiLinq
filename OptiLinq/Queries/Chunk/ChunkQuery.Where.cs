using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public WhereQuery<T[], TWhereOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> Where<TWhereOperator>(TWhereOperator @operator = default) where TWhereOperator : struct, IFunction<T[], bool>
	{
		return new WhereQuery<T[], TWhereOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, @operator);
	}

	public WhereQuery<T[], FuncAsIFunction<T[], bool>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> Where(Func<T[], bool> @operator)
	{
		return new WhereQuery<T[], FuncAsIFunction<T[], bool>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T[], bool>(@operator));
	}
}