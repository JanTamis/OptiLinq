namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public DistinctQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>, TOtherComparer> Distinct<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>, TOtherComparer>(ref this, comparer);
	}
}