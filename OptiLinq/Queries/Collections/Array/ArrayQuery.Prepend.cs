namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public PrependQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, in item);
	}
}