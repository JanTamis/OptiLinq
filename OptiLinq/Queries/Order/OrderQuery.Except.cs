using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ExceptQuery<T, EqualityComparer<T>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TOtherComparer, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TOtherComparer, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(this, other, comparer);
	}
}