namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public DistinctQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TComparer>(ref this, comparer);
	}
}