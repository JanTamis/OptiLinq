namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}