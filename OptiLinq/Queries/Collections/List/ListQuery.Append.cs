namespace OptiLinq;

public partial struct ListQuery<T>
{
	public AppendQuery<T, ListQuery<T>, List<T>.Enumerator> Append(in T element)
	{
		return new AppendQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this, in element);
	}
}