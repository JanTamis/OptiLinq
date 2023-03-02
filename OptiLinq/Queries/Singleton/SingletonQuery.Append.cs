namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public AppendQuery<T, SingletonQuery<T>, SingletonEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, SingletonQuery<T>, SingletonEnumerator<T>>(ref this, in element);
	}
}