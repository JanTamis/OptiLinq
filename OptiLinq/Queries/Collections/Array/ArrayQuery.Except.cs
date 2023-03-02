using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ExceptQuery<T, EqualityComparer<T>, ArrayQuery<T>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, ArrayQuery<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, ArrayQuery<T>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, ArrayQuery<T>, TOtherQuery>(this, other, comparer);
	}
}