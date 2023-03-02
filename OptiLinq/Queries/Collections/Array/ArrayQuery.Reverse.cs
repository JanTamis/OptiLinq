namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ReverseQuery<T, ArrayQuery<T>, ArrayEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, ArrayQuery<T>, ArrayEnumerator<T>>(ref this);
	}
}