namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}