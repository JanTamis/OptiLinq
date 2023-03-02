namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Shuffle(int? seed = null)
	{
		return this;
	}
}