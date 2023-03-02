namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public ChunkQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, chunkSize);
	}
}