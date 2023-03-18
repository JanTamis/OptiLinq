using System.Numerics;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public SkipQuery<TCount, T, EnumerableQuery<T>, IEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, EnumerableQuery<T>, IEnumerator<T>>(ref this, count);
	}
}