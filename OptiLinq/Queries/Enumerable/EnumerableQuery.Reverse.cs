namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ReverseQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this);
	}
}