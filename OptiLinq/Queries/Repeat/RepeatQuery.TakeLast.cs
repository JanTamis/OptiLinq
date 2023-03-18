namespace OptiLinq;

public partial struct RepeatQuery<T>
{
	public TakeLastQuery<T, RepeatQuery<T>, RepeatEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, RepeatQuery<T>, RepeatEnumerator<T>>(this, count);
	}
}