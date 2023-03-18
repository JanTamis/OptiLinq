namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ShuffleQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>>(ref this, seed);
	}
}