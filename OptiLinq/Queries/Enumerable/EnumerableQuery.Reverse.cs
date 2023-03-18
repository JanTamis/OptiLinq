namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ReverseQuery<T, EnumerableQuery<T>, IEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this);
	}
}