namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ChunkQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>> Chunk(int chunkSize)
	{
		return new ChunkQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>>(ref this, chunkSize);
	}
}