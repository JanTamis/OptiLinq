namespace OptiLinq;

public partial struct RangeQuery
{
	public ShuffleQuery<int, RangeQuery, RangeEnumerator> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<int, RangeQuery, RangeEnumerator>(ref this, seed);
	}
}