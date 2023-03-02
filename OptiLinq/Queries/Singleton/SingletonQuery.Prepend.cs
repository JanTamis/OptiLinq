namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public PrependQuery<T, SingletonQuery<T>, SingletonEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, in item);
	}
}