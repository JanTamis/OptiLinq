using System.Numerics;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public SkipQuery<TCount, T, HashSetQuery<T>, HashSet<T>.Enumerator> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, count);
	}
}