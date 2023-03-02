namespace OptiLinq;

public partial struct TakeLastQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, TakeLastQuery<T, TBaseQuery, TBaseEnumerator>, TakeLastEnumerator<T, TBaseEnumerator>>(ref this, defaultValue);
	}
}