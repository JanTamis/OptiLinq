using System.Numerics;

namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public SkipQuery<TSkipCount, T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Skip<TSkipCount>(TSkipCount count)
		where TSkipCount : IBinaryInteger<TSkipCount>
	{
		return new SkipQuery<TSkipCount, T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(ref this, count);
	}
}