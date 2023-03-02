namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}