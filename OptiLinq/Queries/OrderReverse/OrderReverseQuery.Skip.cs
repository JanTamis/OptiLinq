using System.Numerics;

namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SkipQuery<TCount, T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>>(ref this, count);
	}
}