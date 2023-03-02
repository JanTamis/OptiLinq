namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ChunkQuery<T, ListQuery<T>, ListEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this, chunkSize);
	}
}