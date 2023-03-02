using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public UnionQuery<T, RepeatQuery<T>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, RepeatQuery<T>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, RepeatQuery<T>, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, RepeatQuery<T>, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}