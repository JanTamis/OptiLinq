using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ChunkQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ExceptQuery<T[], EqualityComparer<T[]>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T[]>
	{
		return new ExceptQuery<T[], EqualityComparer<T[]>, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, EqualityComparer<T[]>.Default);
	}

	public ExceptQuery<T[], TComparer, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T[]>
		where TComparer : IEqualityComparer<T[]>
	{
		return new ExceptQuery<T[], TComparer, ChunkQuery<T, TBaseQuery, TBaseEnumerator>, TOtherQuery>(this, other, comparer);
	}
}