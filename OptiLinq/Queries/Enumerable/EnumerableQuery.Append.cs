namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public AppendQuery<T, EnumerableQuery<T>, IEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this, in element);
	}
}