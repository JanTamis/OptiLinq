namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public DistinctQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<T>> Distinct()
	{
		return new DistinctQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<T>>(ref this, EqualityComparer<T>.Default);
	}

	public DistinctQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer> Distinct<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>
	{
		return new DistinctQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer>(ref this, comparer);
	}
}