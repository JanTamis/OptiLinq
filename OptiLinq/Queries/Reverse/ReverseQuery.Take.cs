using System.Numerics;

namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TCount, T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}