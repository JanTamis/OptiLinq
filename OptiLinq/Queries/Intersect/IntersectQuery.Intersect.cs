using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public IntersectQuery<T, TOtherComparer, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TOtherComparer, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}