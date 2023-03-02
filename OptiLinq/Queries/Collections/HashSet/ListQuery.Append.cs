namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public AppendQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, in element);
	}
}