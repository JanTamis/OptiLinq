using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public IntersectQuery<T, TComparer, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}