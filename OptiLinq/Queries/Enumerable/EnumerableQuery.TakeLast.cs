namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public TakeLastQuery<T, EnumerableQuery<T>, IEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, EnumerableQuery<T>, IEnumerator<T>>(this, count);
	}
}