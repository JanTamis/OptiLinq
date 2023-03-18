using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public IntersectQuery<T, TComparer, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}