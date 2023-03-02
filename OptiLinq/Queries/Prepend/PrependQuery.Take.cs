using System.Numerics;

namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TCount, T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}