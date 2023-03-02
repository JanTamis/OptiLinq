namespace OptiLinq;

public partial struct RangeQuery
{
	public MemoizeQuery<int, RangeQuery, RangeEnumerator> Memoize()
	{
		return new MemoizeQuery<int, RangeQuery, RangeEnumerator>(ref this);
	}
}