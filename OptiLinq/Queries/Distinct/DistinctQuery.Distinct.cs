namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public DistinctQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer> Distinct<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer>(ref this, comparer);
	}
}