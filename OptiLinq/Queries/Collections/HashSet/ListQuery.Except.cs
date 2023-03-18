using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ExceptQuery<T, EqualityComparer<T>, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, HashSetQuery<T>, HashSet<T>.Enumerator, TOtherQuery>(this, other, comparer);
	}
}