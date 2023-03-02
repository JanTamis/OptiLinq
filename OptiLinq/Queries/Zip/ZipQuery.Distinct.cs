namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public DistinctQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TResult>> Distinct()
	{
		return new DistinctQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TResult>>(ref this, EqualityComparer<TResult>.Default);
	}

	public DistinctQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer> Distinct<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IEqualityComparer<TResult>
	{
		return new DistinctQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer>(ref this, comparer);
	}
}