namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public MemoizeQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>> Memoize()
	{
		return new MemoizeQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>>(ref this);
	}
}