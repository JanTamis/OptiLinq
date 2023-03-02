using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct RandomQuery
{
	public ExceptQuery<int, EqualityComparer<int>, RandomQuery, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<int>
	{
		return new ExceptQuery<int, EqualityComparer<int>, RandomQuery, TOtherQuery>(this, other, EqualityComparer<int>.Default);
	}

	public ExceptQuery<int, TComparer, RandomQuery, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<int>
		where TComparer : IEqualityComparer<int>
	{
		return new ExceptQuery<int, TComparer, RandomQuery, TOtherQuery>(this, other, comparer);
	}
}