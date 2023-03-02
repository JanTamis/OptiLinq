namespace OptiLinq;

public partial struct ListQuery<T>
{
	public DefaultIfEmptyQuery<T, ListQuery<T>, ListEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this, defaultValue);
	}
}