namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public SingletonQuery<T> DefaultIfEmpty(in T defaultValue = default)
	{
		return new SingletonQuery<T>(defaultValue);
	}
}