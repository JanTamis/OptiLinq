using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public IntersectQuery<T, TComparer, GenerateQuery<T, TOperator>, TOtherQuery> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new IntersectQuery<T, TComparer, GenerateQuery<T, TOperator>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<T, EqualityComparer<T>, GenerateQuery<T, TOperator>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return new IntersectQuery<T, EqualityComparer<T>, GenerateQuery<T, TOperator>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}
}