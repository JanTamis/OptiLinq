using System.Numerics;

namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public SkipQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, count);
	}
}