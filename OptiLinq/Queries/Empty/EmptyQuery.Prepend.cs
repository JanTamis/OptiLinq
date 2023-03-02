namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public SingletonQuery<T> Prepend(in T item)
	{
		return new SingletonQuery<T>(item);
	}
}