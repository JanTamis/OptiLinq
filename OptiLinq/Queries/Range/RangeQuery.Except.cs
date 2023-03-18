using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RangeQuery
{
	public ExceptQuery<int, EqualityComparer<int>, RangeQuery, RangeEnumerator, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ExceptQuery<int, EqualityComparer<int>, RangeQuery, RangeEnumerator, TOtherQuery>(this, other, EqualityComparer<int>.Default);
	}

	public ExceptQuery<int, TComparer, RangeQuery, RangeEnumerator, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new ExceptQuery<int, TComparer, RangeQuery, RangeEnumerator, TOtherQuery>(this, other, comparer);
	}
}