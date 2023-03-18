namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ChunkQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, chunkSize);
	}
}