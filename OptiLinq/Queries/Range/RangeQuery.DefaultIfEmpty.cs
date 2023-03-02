namespace OptiLinq;

public partial struct RangeQuery
{
	public DefaultIfEmptyQuery<int, RangeQuery, RangeEnumerator> DefaultIfEmpty(in int defaultValue = default)
	{
		return new DefaultIfEmptyQuery<int, RangeQuery, RangeEnumerator>(ref this, defaultValue);
	}
}