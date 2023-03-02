namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public MemoizeQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>> Memoize()
	{
		return new MemoizeQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>>(ref this);
	}
}