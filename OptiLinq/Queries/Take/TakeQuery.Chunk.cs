namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(ref this, chunkSize);
	}
}