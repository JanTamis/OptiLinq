namespace OptiLinq;

public partial struct IListQuery<T>
{
	public ReverseQuery<T, IListQuery<T>, IListEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, IListQuery<T>, IListEnumerator<T>>(ref this);
	}
}