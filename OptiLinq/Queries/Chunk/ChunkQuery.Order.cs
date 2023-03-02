namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T[]>
	{
		return new OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, Comparer<T[]>> Order()
	{
		return new OrderQuery<T[], ChunkQuery<T, TBaseQuery, TBaseEnumerator>, ChunkEnumerator<T, TBaseEnumerator>, Comparer<T[]>>(ref this, Comparer<T[]>.Default);
	}
}