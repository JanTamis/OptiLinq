using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public ExceptQuery<int, EqualityComparer<int>, RangeQuery, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ExceptQuery<int, EqualityComparer<int>, RangeQuery, TOtherQuery>(this, other, EqualityComparer<int>.Default);
	}

	public ExceptQuery<int, TComparer, RangeQuery, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new ExceptQuery<int, TComparer, RangeQuery, TOtherQuery>(this, other, comparer);
	}
}