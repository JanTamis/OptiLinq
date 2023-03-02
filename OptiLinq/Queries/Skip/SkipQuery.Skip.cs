using System.Numerics;

namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TSkipCount, T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Skip<TSkipCount>(TSkipCount count)
		where TSkipCount : IBinaryInteger<TSkipCount>
	{
		return new SkipQuery<TSkipCount, T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(ref this, count);
	}

	public SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator> Skip(TCount count)
	{
		return new SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, count + _count);
	}
}