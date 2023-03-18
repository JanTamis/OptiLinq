using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public IntersectQuery<T, TComparer, ListQuery<T>, List<T>.Enumerator, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, ListQuery<T>, List<T>.Enumerator, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, ListQuery<T>, List<T>.Enumerator, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, ListQuery<T>, List<T>.Enumerator, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}