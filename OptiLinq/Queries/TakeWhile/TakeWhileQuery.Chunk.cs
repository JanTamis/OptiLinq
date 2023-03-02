namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this, chunkSize);
	}
}