namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public PrependQuery<T, EnumerableQuery<T>, IEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this, in item);
	}
}