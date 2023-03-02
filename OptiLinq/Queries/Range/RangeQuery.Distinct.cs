namespace OptiLinq;

public partial struct RangeQuery
{
	public DistinctQuery<int, RangeQuery, RangeEnumerator, EqualityComparer<int>> Distinct()
	{
		return new DistinctQuery<int, RangeQuery, RangeEnumerator, EqualityComparer<int>>(ref this, EqualityComparer<int>.Default);
	}

	public DistinctQuery<int, RangeQuery, RangeEnumerator, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		return new DistinctQuery<int, RangeQuery, RangeEnumerator, TComparer>(ref this, comparer);
	}
}