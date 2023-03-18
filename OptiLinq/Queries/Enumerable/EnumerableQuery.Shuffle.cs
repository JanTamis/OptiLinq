namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public ShuffleQuery<T, EnumerableQuery<T>, IEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, EnumerableQuery<T>, IEnumerator<T>>(ref this, seed);
	}
}