using System.Numerics;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeQuery<TCount, T, HashSetQuery<T>, HashSet<T>.Enumerator> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, count);
	}
}