using System.Numerics;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TSkipCount, T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Skip<TSkipCount>(TSkipCount count)
		where TSkipCount : IBinaryInteger<TSkipCount>
	{
		return new SkipQuery<TSkipCount, T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}