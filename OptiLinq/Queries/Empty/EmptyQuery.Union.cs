using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Union<TOtherQuery>(TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return this;
	}

	public EmptyQuery<T> Union<TOtherQuery, TComparer>(TOtherQuery other, TComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return this;
	}
}