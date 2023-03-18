namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ShuffleQuery<T, ListQuery<T>, List<T>.Enumerator> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, ListQuery<T>, List<T>.Enumerator>(ref this, seed);
	}
}