namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeLastQuery<T, ListQuery<T>, List<T>.Enumerator> TakeLast(int count)
	{
		return new TakeLastQuery<T, ListQuery<T>, List<T>.Enumerator>(this, count);
	}
}