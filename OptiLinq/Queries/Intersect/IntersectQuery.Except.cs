using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public ExceptQuery<T, EqualityComparer<T>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TOtherComparer, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TOtherComparer, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}
}