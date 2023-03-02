using System.Numerics;

namespace OptiLinq;

public partial struct RandomQuery
{
	public SkipQuery<TCount, int, RandomQuery, RandomEnumerator> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, int, RandomQuery, RandomEnumerator>(ref this, count);
	}
}