using System.Numerics;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public SkipQuery<TCount, T, HashSetQuery<T>, HashSetEnumerator<T>> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, count);
	}
}