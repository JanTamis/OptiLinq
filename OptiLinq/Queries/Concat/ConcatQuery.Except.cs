using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ExceptQuery<T, EqualityComparer<T>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TComparer, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery> Except<TOtherQuery, TComparer>(in TOtherQuery other, TComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TComparer, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}
}