using System.Numerics;

namespace OptiLinq;

public partial struct RangeQuery
{
	public TakeLastQuery<int, RangeQuery, RangeEnumerator> TakeLast(int count)
	{
		return new TakeLastQuery<int, RangeQuery, RangeEnumerator>(this, count);
	}
}