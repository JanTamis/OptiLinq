namespace OptiLinq;

public partial struct SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<TResult, SelectQuery<T, TResult, TOperator, TBaseQuery, TBaseEnumerator>, SelectEnumerator<T, TResult, TOperator, TBaseEnumerator>>(ref this, chunkSize);
	}
}