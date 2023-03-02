using System.Numerics;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public SkipQuery<TCount, T, ArrayQuery<T>, ArrayEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, count);
	}
}