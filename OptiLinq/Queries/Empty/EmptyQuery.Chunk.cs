namespace OptiLinq;

public partial struct EmptyQuery<T>
{
	public EmptyQuery<T[]> Chunk(int chunkSize)
	{
		return new EmptyQuery<T[]>();
	}
}