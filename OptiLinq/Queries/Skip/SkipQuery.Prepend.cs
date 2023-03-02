namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public PrependQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> Prepend(in T item)
	{
		return new PrependQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(ref this, in item);
	}
}