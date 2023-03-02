using System.Numerics;

namespace OptiLinq;

public partial struct RangeQuery
{
	public SkipQuery<TCount, int, RangeQuery, RangeEnumerator> Skip<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new SkipQuery<TCount, int, RangeQuery, RangeEnumerator>(ref this, count);
	}
}