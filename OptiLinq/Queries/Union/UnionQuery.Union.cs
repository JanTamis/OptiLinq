using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>
{
	public UnionQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, UnionQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery, TComparer>, UnionEnumerator<T, TFirstEnumerator, TComparer>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}