namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ChunkQuery<T, EnumerableQuery<T>, IEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this, chunkSize);
	}
}