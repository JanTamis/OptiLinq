namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>
{
	public MemoizeQuery<T, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> Memoize()
	{
		return new MemoizeQuery<T, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(ref this);
	}
}