using System.Numerics;

namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SkipQuery<TCount, T, SingletonQuery<T>, SingletonEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, count);
	}
}