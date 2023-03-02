namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public AppendQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>> Append(in T element)
	{
		return new AppendQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>>(ref this, in element);
	}
}