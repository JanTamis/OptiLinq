namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public AppendQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> Append(in T element)
	{
		return new AppendQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, in element);
	}
}