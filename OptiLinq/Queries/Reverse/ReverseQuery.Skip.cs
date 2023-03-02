using System.Numerics;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TCount, T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}