using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SelectQuery<T[], TResult, TSelectOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> Select<TSelectOperator, TResult>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T[], TResult>
	{
		return new SelectQuery<T[], TResult, TSelectOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T[], T[], TSelectOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> Select<TSelectOperator>(TSelectOperator selector = default) where TSelectOperator : struct, IFunction<T[], T[]>
	{
		return new SelectQuery<T[], T[], TSelectOperator, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, selector);
	}

	public SelectQuery<T[], TResult, FuncAsIFunction<T[], TResult>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> Select<TResult>(Func<T[], TResult> selector)
	{
		return new SelectQuery<T[], TResult, FuncAsIFunction<T[], TResult>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, new FuncAsIFunction<T[], TResult>(selector));
	}
}