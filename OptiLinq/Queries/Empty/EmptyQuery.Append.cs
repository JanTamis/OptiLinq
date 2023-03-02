namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public SingletonQuery<T> Append(in T element)
	{
		return new SingletonQuery<T>(element);
	}
}