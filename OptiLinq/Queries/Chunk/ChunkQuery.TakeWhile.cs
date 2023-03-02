using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeWhileQuery<T[], TOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> TakeWhile<TOperator>(TOperator @operator)
		where TOperator : struct, IFunction<T[], bool>
	{
		return new TakeWhileQuery<T[], TOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(this, @operator);
	}

	public TakeWhileQuery<T[], FuncAsIFunction<T[], bool>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> TakeWhile(Func<T[], bool> @operator)
	{
		return new TakeWhileQuery<T[], FuncAsIFunction<T[], bool>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(this, new FuncAsIFunction<T[], bool>(@operator));
	}
}