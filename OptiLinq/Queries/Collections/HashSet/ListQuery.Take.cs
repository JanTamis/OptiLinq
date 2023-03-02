using System.Numerics;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeQuery<TCount, T, HashSetQuery<T>, HashSetEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, count);
	}
}