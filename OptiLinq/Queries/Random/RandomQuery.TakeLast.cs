using System.Numerics;

namespace OptiLinq;

public partial struct RandomQuery
{
	public TakeLastQuery<int, RandomQuery, RandomEnumerator> TakeLast(int count)
	{
		return new TakeLastQuery<int, RandomQuery, RandomEnumerator>(this, count);
	}
}