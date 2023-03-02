using System.Numerics;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public TakeQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, count);
	}
}