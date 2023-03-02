namespace OptiLinq;

public partial struct RangeQuery
{
	public ChunkQuery<int, RangeQuery, RangeEnumerator> Chunk(int chunkSize)
	{
		return new ChunkQuery<int, RangeQuery, RangeEnumerator>(ref this, chunkSize);
	}
}