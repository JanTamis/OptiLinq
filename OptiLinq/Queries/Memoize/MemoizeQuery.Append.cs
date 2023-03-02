namespace OptiLinq;

public partial struct MemoizeQuery<T, TBaseQuery, TBaseEnumerator>
{
	public AppendQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, MemoizeQuery<T, TBaseQuery, TBaseEnumerator>, MemoizeEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}