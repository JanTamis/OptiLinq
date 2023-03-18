namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public PrependQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> Prepend(in T item)
	{
		return new PrependQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, in item);
	}
}