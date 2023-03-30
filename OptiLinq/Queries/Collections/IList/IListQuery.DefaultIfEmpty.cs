namespace OptiLinq;

public partial struct IListQuery<T>
{
	public DefaultIfEmptyQuery<T, IListQuery<T>, IListEnumerator<T>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, IListQuery<T>, IListEnumerator<T>>(ref this, defaultValue);
	}
}