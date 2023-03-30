namespace OptiLinq;

public partial struct IListQuery<T>
{
	public TakeLastQuery<T, IListQuery<T>, IListEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, IListQuery<T>, IListEnumerator<T>>(this, count);
	}
}