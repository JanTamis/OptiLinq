using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public IntersectQuery<T, TComparer, EnumerableQuery<T>, IEnumerator<T>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, EnumerableQuery<T>, IEnumerator<T>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, EnumerableQuery<T>, IEnumerator<T>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, EnumerableQuery<T>, IEnumerator<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}