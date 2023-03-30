namespace OptiLinq;

public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ShuffleQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>>(ref this, seed);
	}
}