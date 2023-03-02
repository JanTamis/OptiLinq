namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public DistinctQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, RepeatQuery<T>, RepeatEnumerator<T>, TComparer>(ref this, comparer);
	}
}