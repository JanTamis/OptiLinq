namespace OptiLinq;

public partial struct RandomQuery
{
	public ReverseQuery<int, RandomQuery, RandomEnumerator> Reverse()
	{
		return new ReverseQuery<int, RandomQuery, RandomEnumerator>(ref this);
	}
}