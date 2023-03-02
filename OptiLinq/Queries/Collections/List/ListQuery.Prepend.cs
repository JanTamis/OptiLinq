namespace OptiLinq;

public partial struct ListQuery<T>
{
	public PrependQuery<T, ListQuery<T>, ListEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this, in item);
	}
}