namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public ChunkQuery<T, SingletonQuery<T>, SingletonEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, chunkSize);
	}
}