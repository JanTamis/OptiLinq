namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ReverseQuery<T, ListQuery<T>, List<T>.Enumerator> Reverse()
	{
		return new ReverseQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this);
	}
}