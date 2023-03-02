namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public AppendQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, in element);
	}
}