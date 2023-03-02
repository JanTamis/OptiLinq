namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>> DefaultIfEmpty(in T[] defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>>(ref this, defaultValue);
	}
}