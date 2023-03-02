namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public PrependQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>> Prepend(in T item)
	{
		return new PrependQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>>(ref this, in item);
	}
}