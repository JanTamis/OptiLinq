namespace OptiLinq;

public partial struct ShuffleQuery<T, TBaseQuery, TBaseEnumerator>
{
	public DefaultIfEmptyQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ShuffleQuery<T, TBaseQuery, TBaseEnumerator>, ShuffleEnumerator<T, TBaseEnumerator>>(ref this, defaultValue);
	}
}