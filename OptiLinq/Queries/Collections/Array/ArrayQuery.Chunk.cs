namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ChunkQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, chunkSize);
	}
}