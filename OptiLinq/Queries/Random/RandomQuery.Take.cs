using System.Numerics;

namespace OptiLinq;

public partial struct RandomQuery
{
	public TakeQuery<TCount, int, RandomQuery, RandomEnumerator> Take<TCount>(TCount count)
		where TCount : IBinaryInteger<TCount>
	{
		return new TakeQuery<TCount, int, RandomQuery, RandomEnumerator>(ref this, count);
	}
}