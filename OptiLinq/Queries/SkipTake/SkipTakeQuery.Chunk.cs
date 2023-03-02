namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
{
	public ChunkQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(ref this, chunkSize);
	}
}