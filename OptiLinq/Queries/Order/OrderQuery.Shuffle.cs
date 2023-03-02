namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ShuffleQuery<T, TBaseQuery, TBaseEnumerator> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, TBaseQuery, TBaseEnumerator>(ref _baseEnumerable, seed);
	}
}