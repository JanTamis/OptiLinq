namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public DistinctQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer>(ref this, comparer);
	}
}