namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public ShuffleQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, HashSetQuery<T>, HashSet<T>.Enumerator>(ref this, seed);
	}
}