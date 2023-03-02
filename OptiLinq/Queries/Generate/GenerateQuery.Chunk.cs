namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public ChunkQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>>(ref this, chunkSize);
	}
}