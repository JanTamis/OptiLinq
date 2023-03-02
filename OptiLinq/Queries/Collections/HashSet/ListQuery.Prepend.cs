namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public PrependQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, in item);
	}
}