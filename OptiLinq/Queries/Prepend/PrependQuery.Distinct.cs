namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}