namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public DistinctQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, TOtherComparer> Distinct<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, TOtherComparer>(ref this, comparer);
	}
}