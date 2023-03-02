using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public UnionQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}