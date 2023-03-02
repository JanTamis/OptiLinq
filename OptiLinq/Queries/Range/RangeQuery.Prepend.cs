namespace OptiLinq;

public partial struct RangeQuery
{
	public PrependQuery<int, RangeQuery, RangeEnumerator> Prepend(in int item)
	{
		return new PrependQuery<int, RangeQuery, RangeEnumerator>(ref this, in item);
	}
}