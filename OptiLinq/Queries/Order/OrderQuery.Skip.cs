using System.Numerics;

namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SkipQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>>(ref this, count);
	}
}