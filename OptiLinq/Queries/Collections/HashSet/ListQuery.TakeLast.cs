namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeLastQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> TakeLast(int count)
	{
		return new TakeLastQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(this, count);
	}
}