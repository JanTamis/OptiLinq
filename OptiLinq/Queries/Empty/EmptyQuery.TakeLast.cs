namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T> TakeLast(int count)
	{
		return this;
	}
}