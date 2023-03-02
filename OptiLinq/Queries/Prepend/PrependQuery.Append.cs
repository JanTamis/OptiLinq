namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}