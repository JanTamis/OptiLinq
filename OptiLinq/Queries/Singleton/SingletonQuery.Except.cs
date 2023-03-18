using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public ExceptQuery<T, EqualityComparer<T>, SingletonQuery<T>, SingletonEnumerator<T>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, SingletonQuery<T>, SingletonEnumerator<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, SingletonQuery<T>, SingletonEnumerator<T>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, SingletonQuery<T>, SingletonEnumerator<T>, TOtherQuery>(this, other, comparer);
	}
}