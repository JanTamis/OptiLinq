namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public TakeLastQuery<T, SingletonQuery<T>, SingletonEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, SingletonQuery<T>, SingletonEnumerator<T>>(this, count);
	}
}