namespace OptiLinq;

public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public MemoizeQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>> Memoize()
	{
		return new MemoizeQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>>(ref this);
	}
}