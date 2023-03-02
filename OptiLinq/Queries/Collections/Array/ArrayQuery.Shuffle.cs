namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ShuffleQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, seed);
	}
}