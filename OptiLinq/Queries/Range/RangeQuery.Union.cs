using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public UnionQuery<int, RangeQuery, RangeEnumerator, TOtherQuery, EqualityComparer<int>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new UnionQuery<int, RangeQuery, RangeEnumerator, TOtherQuery, EqualityComparer<int>>(ref this, ref other, EqualityComparer<int>.Default);
	}

	public UnionQuery<int, RangeQuery, RangeEnumerator, TOtherQuery, TComparer> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new UnionQuery<int, RangeQuery, RangeEnumerator, TOtherQuery, TComparer>(ref this, ref other, comparer);
	}
}