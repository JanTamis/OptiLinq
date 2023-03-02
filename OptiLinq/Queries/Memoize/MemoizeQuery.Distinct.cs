namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}