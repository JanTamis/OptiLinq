using System.Numerics;

namespace OptiLinq;

public partial struct TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TTakeCount, T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>> Take<TTakeCount>(TTakeCount count)
		where TTakeCount : IBinaryInteger<TTakeCount>
	{
		return new TakeQuery<TTakeCount, T, TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>, TakeEnumerator<TCount, T, TBaseEnumerator>>(ref this, count);
	}

	public TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator> Take(TCount count)
	{
		return new TakeQuery<TCount, T, TBaseQuery, TBaseEnumerator>(ref _baseQuery, TCount.Min(_count, count));
	}
}