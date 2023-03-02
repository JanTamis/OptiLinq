namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public PrependQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, in item);
	}
}