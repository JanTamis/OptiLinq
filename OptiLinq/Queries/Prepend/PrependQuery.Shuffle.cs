namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ShuffleQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>>(ref this, seed);
	}
}