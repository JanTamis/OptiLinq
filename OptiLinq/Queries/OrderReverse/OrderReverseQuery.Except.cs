using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ExceptQuery<T, EqualityComparer<T>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new ExceptQuery<T, EqualityComparer<T>, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(this, other, EqualityComparer<T>.Default);
	}

	public ExceptQuery<T, TOtherComparer, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new ExceptQuery<T, TOtherComparer, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery>(this, other, comparer);
	}
}