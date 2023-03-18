using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RandomQuery
{
	public IntersectQuery<int, TComparer, RandomQuery, RandomEnumerator, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new IntersectQuery<int, TComparer, RandomQuery, RandomEnumerator, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<int, EqualityComparer<int>, RandomQuery, RandomEnumerator, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<int>
	{
		return new IntersectQuery<int, EqualityComparer<int>, RandomQuery, RandomEnumerator, TOtherQuery>(this, other, EqualityComparer<int>.Default);
	}
}