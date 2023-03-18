namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public TakeLastQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(this, count);
	}
}