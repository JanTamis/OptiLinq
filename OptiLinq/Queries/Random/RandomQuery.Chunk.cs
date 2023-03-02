namespace OptiLinq;

public partial struct RandomQuery
{
	public ChunkQuery<int, RandomQuery, RandomEnumerator> Chunk(int chunkSize)
	{
		return new ChunkQuery<int, RandomQuery, RandomEnumerator>(ref this, chunkSize);
	}
}