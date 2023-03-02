using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Intersect<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return this;
	}

	public EmptyQuery<T> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<T>
	{
		return this;
	}
}