namespace OptiLinq;

public partial struct SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, SkipQuery<TCount, T, TBaseQuery, TBaseEnumerator>, SkipEnumerator<TCount, T, TBaseEnumerator>>(ref this, defaultValue);
	}
}