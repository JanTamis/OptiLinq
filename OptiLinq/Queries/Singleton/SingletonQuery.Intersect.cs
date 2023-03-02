using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public IntersectQuery<T, TComparer, SingletonQuery<T>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, SingletonQuery<T>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, SingletonQuery<T>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, SingletonQuery<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}