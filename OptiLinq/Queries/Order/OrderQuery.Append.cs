namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public AppendQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderEnumerator<T, TComparer, TBaseEnumerator>>(ref this, in element);
	}
}