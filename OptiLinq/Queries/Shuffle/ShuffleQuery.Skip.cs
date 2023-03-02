using System.Numerics;

namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TCount, T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}