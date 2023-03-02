namespace OptiLinq;

public partial struct OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public PrependQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>> Prepend(in T item)
	{
		return new PrependQuery<T, OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, OrderReverseEnumerator<T, TComparer, TBaseEnumerator>>(ref this, in item);
	}
}