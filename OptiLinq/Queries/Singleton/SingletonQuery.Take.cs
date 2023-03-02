using System.Numerics;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public TakeQuery<TCount, T, SingletonQuery<T>, SingletonEnumerator<T>> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, T, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, count);
	}
}