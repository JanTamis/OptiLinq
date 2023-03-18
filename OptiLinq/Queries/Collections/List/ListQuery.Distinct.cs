namespace OptiLinq;

public partial struct ListQuery<T>
{
	public DistinctQuery<T, ListQuery<T>, List<T>.Enumerator, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ListQuery<T>, List<T>.Enumerator, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer>(ref this, comparer);
	}
}