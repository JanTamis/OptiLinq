namespace OptiLinq;

public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}