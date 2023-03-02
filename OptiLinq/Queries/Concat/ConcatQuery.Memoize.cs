namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public MemoizeQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> Memoize()
	{
		return new MemoizeQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(ref this);
	}
}