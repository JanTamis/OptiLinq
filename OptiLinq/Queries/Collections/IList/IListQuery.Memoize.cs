namespace OptiLinq;

public partial struct IListQuery<T>
{
	public IListQuery<T> Memoize()
	{
		return this;
	}
}