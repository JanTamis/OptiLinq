namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public DefaultIfEmptyQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, defaultValue);
	}
}