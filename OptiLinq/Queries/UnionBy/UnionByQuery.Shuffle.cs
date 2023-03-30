namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>
{
	public ShuffleQuery<T, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>>(ref this, seed);
	}
}