namespace OptiLinq;

public partial struct IListQuery<T>
{
	public ChunkQuery<T, IListQuery<T>, IListEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, IListQuery<T>, IListEnumerator<T>>(ref this, chunkSize);
	}
}