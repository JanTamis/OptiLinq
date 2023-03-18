namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public DefaultIfEmptyQuery<T, EnumerableQuery<T>, IEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this, defaultValue);
	}
}