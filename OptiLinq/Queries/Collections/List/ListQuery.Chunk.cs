namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ChunkQuery<T, ListQuery<T>, List<T>.Enumerator> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this, chunkSize);
	}
}