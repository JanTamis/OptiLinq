using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public IntersectQuery<T, TComparer, ListQuery<T>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, ListQuery<T>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, ListQuery<T>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, ListQuery<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}