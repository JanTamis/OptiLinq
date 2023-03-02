namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public ShuffleQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> Shuffle(int? seed = null)
	{
		return new ShuffleQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(ref this, seed);
	}
}