namespace OptiLinq;

public partial struct RandomQuery
{
	public ShuffleQuery<int, RandomQuery, RandomEnumerator> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<int, RandomQuery, RandomEnumerator>(ref this, seed);
	}
}