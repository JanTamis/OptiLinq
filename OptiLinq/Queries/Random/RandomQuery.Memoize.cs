namespace OptiLinq;

public partial struct RandomQuery
{
	public MemoizeQuery<int, RandomQuery, RandomEnumerator> Memoize()
	{
		return new MemoizeQuery<int, RandomQuery, RandomEnumerator>(ref this);
	}
}