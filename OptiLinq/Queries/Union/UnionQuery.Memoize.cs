namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public MemoizeQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> Memoize()
	{
		return new MemoizeQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(ref this);
	}
}