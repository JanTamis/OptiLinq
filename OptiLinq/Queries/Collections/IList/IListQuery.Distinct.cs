namespace OptiLinq;

public partial struct IListQuery<T>
{
	public DistinctQuery<T, IListQuery<T>, IListEnumerator<T>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, IListQuery<T>, IListEnumerator<T>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, IListQuery<T>, IListEnumerator<T>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, IListQuery<T>, IListEnumerator<T>, TComparer>(ref this, comparer);
	}
}