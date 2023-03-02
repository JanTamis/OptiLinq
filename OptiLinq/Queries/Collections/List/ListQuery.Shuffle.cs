namespace OptiLinq;

public partial struct ListQuery<T>
{
	public ShuffleQuery<T, ListQuery<T>, ListEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, ListQuery<T>, ListEnumerator<T>>(ref this, seed);
	}
}