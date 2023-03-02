namespace OptiLinq;

public partial struct ListQuery<T>
{
	public DistinctQuery<T, ListQuery<T>, ListEnumerator<T>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ListQuery<T>, ListEnumerator<T>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ListQuery<T>, ListEnumerator<T>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ListQuery<T>, ListEnumerator<T>, TComparer>(ref this, comparer);
	}
}