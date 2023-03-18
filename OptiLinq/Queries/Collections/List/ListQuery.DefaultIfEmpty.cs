namespace OptiLinq;

public partial struct ListQuery<T>
{
	public DefaultIfEmptyQuery<T, ListQuery<T>, List<T>.Enumerator> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this, defaultValue);
	}
}