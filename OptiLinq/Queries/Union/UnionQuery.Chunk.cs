namespace OptiLinq;

public partial struct UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>
{
	public ChunkQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, UnionQuery<T, TFirstQuery, TSecondQuery, TComparer>, UnionEnumerator<T, TComparer>>(ref this, chunkSize);
	}
}