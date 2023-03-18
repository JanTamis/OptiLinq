namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ReverseQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> Reverse()
	{
		return new ReverseQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this);
	}
}