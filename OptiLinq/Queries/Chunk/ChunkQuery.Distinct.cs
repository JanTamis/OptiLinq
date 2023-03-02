namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, EqualityComparer<T[]>> Distinct()
	{
		return new DistinctQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, EqualityComparer<T[]>>(ref this, EqualityComparer<T[]>.Default);
	}

	public DistinctQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T[]>
	{
		return new DistinctQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}