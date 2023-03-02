namespace OptiLinq;

public partial struct RangeQuery
{
	public ReverseQuery<int, RangeQuery, RangeEnumerator> Reverse()
	{
		return new ReverseQuery<int, RangeQuery, RangeEnumerator>(ref this);
	}
}