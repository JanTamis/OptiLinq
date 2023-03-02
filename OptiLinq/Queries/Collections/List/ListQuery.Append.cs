namespace OptiLinq;

public partial struct ListQuery<T>
{
	public AppendQuery<T, ListQuery<T>, ListEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this, in element);
	}
}