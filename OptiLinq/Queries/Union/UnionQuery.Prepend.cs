namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public PrependQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> Prepend(in T item)
	{
		return new PrependQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(ref this, in item);
	}
}