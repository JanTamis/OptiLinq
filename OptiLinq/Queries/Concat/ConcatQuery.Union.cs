using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public UnionQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}