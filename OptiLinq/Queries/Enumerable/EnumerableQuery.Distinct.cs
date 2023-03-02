namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public DistinctQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>, TComparer>(ref this, comparer);
	}
}