namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}