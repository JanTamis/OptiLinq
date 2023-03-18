using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ExceptQuery<T, EqualityComparer<T>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T>, TOtherQuery>(this, other, comparer);
	}
}