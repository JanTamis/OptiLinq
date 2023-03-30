using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public UnionQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}