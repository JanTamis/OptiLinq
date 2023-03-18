using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public IntersectQuery<int, TComparer, RangeQuery, RangeEnumerator, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new IntersectQuery<int, TComparer, RangeQuery, RangeEnumerator, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<int, EqualityComparer<int>, RangeQuery, RangeEnumerator, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<int>
	{
		return new IntersectQuery<int, EqualityComparer<int>, RangeQuery, RangeEnumerator, TOtherQuery>(this, other, EqualityComparer<int>.Default);
	}
}