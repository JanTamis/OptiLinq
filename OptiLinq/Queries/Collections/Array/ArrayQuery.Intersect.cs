using OptiLinq.Comparers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public IntersectQuery<T, TComparer, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, StructEqualityComparer<T>, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, StructEqualityComparer<T>, ArrayQuery<T>, ArrayEnumerator<T>, TOtherQuery>(this, other, new StructEqualityComparer<T>());
	}
}