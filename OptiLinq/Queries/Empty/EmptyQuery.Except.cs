using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return this;
	}

	public EmptyQuery<T> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return this;
	}
}