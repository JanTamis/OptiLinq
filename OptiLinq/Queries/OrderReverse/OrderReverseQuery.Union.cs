using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public UnionQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery, EqualityComparer<T>> Union<TOtherQuery>(ref TOtherQuery other) where TOtherQuery : struct, IOptiQuery<T>
	{
		return new UnionQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery, EqualityComparer<T>>(ref this, ref other, EqualityComparer<T>.Default);
	}

	public UnionQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery, TOtherComparer> Union<TOtherQuery, TOtherComparer>(TOtherQuery other, TOtherComparer comparer = default!)
		where TOtherQuery : struct, IOptiQuery<T>
		where TOtherComparer : IEqualityComparer<T>
	{
		return new UnionQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, TOtherQuery, TOtherComparer>(ref this, ref other, comparer);
	}
}