namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public ChunkQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(ref this, chunkSize);
	}
}