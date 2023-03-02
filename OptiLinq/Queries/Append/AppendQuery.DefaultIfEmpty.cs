namespace OptiLinq;

public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>>(ref this, defaultValue);
	}
}