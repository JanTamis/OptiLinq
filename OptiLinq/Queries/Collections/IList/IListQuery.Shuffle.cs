namespace OptiLinq;

public partial struct IListQuery<T>
{
	public ShuffleQuery<T, IListQuery<T>, IListEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, IListQuery<T>, IListEnumerator<T>>(ref this, seed);
	}
}