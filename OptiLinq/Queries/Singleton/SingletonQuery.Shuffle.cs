namespace OptiLinq;

public partial struct SingletonQuery<T>
{
	public SingletonQuery<T> Shuffle(int? seed = null)
	{
		return this;
	}
}