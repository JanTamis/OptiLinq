namespace OptiLinq;

public partial struct ListQuery<T>
{
	public PrependQuery<T, ListQuery<T>, List<T>.Enumerator> Prepend(in T item)
	{
		return new PrependQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this, in item);
	}
}