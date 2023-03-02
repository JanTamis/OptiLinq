using System.Numerics;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public TakeQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, count);
	}
}