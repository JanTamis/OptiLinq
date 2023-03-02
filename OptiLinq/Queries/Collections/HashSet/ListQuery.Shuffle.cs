namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ShuffleQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(ref this, seed);
	}
}