using System.Numerics;

namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TTakeCount, T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Take<TTakeCount>(TTakeCount count)
		where TTakeCount : IBinaryInteger<TTakeCount>
	{
		return new TakeQuery<TTakeCount, T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}