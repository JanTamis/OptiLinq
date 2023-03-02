using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public IntersectQuery<T, TComparer, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}