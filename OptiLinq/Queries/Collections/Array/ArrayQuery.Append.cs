namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public AppendQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this, in element);
	}
}