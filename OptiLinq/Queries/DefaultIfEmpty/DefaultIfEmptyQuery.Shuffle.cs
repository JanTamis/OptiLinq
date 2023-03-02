namespace OptiLinq;

public partial struct DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>
{
	public ShuffleQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, DefaultIfEmptyQuery<T, TBaseQuery, TBaseEnumerator>, DefaultIfEmptyEnumerator<T, TBaseEnumerator>>(ref this, seed);
	}
}