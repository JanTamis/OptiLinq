namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ReverseQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this);
	}
}