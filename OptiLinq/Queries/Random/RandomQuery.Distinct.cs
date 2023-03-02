namespace OptiLinq;

public partial struct RandomQuery
{
	public DistinctQuery<int, RandomQuery, RandomEnumerator, EqualityComparer<int>> Distinct()
	{
		return new DistinctQuery<int, RandomQuery, RandomEnumerator, EqualityComparer<int>>(ref this, EqualityComparer<int>.Default);
	}

	public DistinctQuery<int, RandomQuery, RandomEnumerator, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<int>
	{
		return new DistinctQuery<int, RandomQuery, RandomEnumerator, TComparer>(ref this, comparer);
	}
}