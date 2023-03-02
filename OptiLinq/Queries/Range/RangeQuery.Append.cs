namespace OptiLinq;

public partial struct RangeQuery
{
	public AppendQuery<int, RangeQuery, RangeEnumerator> Append(in int element)
	{
		return new AppendQuery<int, RangeQuery, RangeEnumerator>(ref this, in element);
	}
}