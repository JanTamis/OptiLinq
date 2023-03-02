using System.Numerics;

namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public TakeQuery<TCount, T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, count);
	}
}