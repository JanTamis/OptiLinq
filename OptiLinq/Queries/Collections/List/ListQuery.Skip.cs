using System.Numerics;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public SkipQuery<TCount, T, ListQuery<T>, ListEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ListQuery<T>, ListEnumerator<T>>(ref this, count);
	}
}