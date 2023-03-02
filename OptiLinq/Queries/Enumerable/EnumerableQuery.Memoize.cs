namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public MemoizeQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Memoize()
	{
		return new MemoizeQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this);
	}
}