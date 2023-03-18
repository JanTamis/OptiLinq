namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public MemoizeQuery<T, EnumerableQuery<T>, IEnumerator<T>> Memoize()
	{
		return new MemoizeQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this);
	}
}