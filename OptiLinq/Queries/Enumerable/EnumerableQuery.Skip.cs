using System.Numerics;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public SkipQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, count);
	}
}