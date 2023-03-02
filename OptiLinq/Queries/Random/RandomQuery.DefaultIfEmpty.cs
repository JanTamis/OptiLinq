namespace OptiLinq;

public partial struct RandomQuery
{
	public DefaultIfEmptyQuery<int, RandomQuery, RandomEnumerator> DefaultIfEmpty(in int defaultValue = default)
	{
		return new DefaultIfEmptyQuery<int, RandomQuery, RandomEnumerator>(ref this, defaultValue);
	}
}