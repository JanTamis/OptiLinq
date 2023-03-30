namespace OptiLinq;

public partial struct IListQuery<T>
{
	public PrependQuery<T, IListQuery<T>, IListEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, IListQuery<T>, IListEnumerator<T>>(ref this, in item);
	}
}