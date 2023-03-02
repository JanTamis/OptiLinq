namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>>(ref this, chunkSize);
	}
}