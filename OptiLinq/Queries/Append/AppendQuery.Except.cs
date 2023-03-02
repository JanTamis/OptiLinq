using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ExceptQuery<T, EqualityComparer<T>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, AppendQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, AppendQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, AppendQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, comparer);
	}
}