using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RandomQuery
{
	public UnionQuery<int, RandomQuery, TOtherQuery, EqualityComparer<int>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new UnionQuery<int, RandomQuery, TOtherQuery, EqualityComparer<int>>(ref this, ref other, EqualityComparer<int>.Default);
	}

	public UnionQuery<int, RandomQuery, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new UnionQuery<int, RandomQuery, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}