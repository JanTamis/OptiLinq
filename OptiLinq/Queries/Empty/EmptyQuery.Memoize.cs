namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> Memoize()
	{
		return this;
	}
}