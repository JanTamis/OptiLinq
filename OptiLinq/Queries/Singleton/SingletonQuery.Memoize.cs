namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<T> Memoize()
	{
		return this;
	}
}