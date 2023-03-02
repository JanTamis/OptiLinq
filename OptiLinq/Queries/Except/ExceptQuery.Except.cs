using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public ExceptQuery<T, EqualityComparer<T>, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TOtherComparer, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TOtherComparer, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, TOtherQuery>(this, other, comparer);
	}
}