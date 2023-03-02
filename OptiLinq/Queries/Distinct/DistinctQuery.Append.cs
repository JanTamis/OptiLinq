namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public AppendQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>>(ref this, in element);
	}
}