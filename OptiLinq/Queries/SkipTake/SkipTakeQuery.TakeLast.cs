using System.Numerics;

namespace OptiLinq;

public partial struct SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>
{
	public TakeLastQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>> TakeLast(int count)
	{
		return new TakeLastQuery<T, SkipTakeQuery<TSkipCount, TTakeCount, T, TBaseQuery, TBaseEnumerator>, SkipTakeEnumerator<TSkipCount, TTakeCount, T, TBaseEnumerator>>(this, count);
	}
}