namespace OptiLinq;

public partial struct ReverseQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ReverseQuery<T, TBaseQuery, TBaseEnumerator>, ReverseEnumerator<T, TBaseEnumerator>>(ref this, defaultValue);
	}
}