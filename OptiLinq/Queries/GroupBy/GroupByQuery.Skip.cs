using System.Numerics;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public SkipQuery<TCount, ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, count);
	}
}