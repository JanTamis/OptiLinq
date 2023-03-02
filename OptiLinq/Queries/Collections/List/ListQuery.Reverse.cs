namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ReverseQuery<T, ListQuery<T>, ListEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this);
	}
}