namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ListQuery<T> Memoize()
	{
		return this;
	}
}