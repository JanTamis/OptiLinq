using System.Numerics;

namespace OptiLinq;

public partial struct RangeQuery
{
	public TakeQuery<TCount, int, RangeQuery, RangeEnumerator> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, int, RangeQuery, RangeEnumerator>(ref this, count);
	}
}