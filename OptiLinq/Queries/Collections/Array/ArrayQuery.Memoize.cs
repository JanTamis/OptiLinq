namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public ArrayQuery<T> Memoize()
	{
		return this;
	}
}