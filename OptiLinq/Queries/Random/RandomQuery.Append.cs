namespace OptiLinq;

public partial struct RandomQuery
{
	public AppendQuery<int, RandomQuery, RandomEnumerator> Append(in int element)
	{
		return new AppendQuery<int, RandomQuery, RandomEnumerator>(ref this, in element);
	}
}