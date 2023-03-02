namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ChunkQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, chunkSize);
	}
}