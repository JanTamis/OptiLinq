using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public IntersectQuery<T, TComparer, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}