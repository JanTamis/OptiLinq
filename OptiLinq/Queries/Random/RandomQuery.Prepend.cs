namespace OptiLinq;

public partial struct RandomQuery
{
	public PrependQuery<int, RandomQuery, RandomEnumerator> Prepend(in int item)
	{
		return new PrependQuery<int, RandomQuery, RandomEnumerator>(ref this, in item);
	}
}