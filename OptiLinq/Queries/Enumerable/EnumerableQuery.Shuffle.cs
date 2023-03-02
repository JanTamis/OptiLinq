namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ShuffleQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(ref this, seed);
	}
}