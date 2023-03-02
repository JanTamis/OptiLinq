namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ChunkQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>>(ref this, chunkSize);
	}
}