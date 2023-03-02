namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<T> DefaultIfEmpty(in T defaultValue = default)
	{
		return this;
	}
}