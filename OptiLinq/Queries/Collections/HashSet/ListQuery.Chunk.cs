namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ChunkQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, chunkSize);
	}
}