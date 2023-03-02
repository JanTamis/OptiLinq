using System.Numerics;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public TakeQuery<TCount, T, ArrayQuery<T>, ArrayEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, count);
	}
}