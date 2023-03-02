namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer> Reverse()
	{
		return new OrderReverseQuery<T, TBaseQuery, TBaseEnumerator, TComparer>(ref _baseEnumerable, _comparer);
	}
}