using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public IntersectQuery<T, TOtherComparer, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, TOtherQuery> Intersect<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TOtherComparer, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}