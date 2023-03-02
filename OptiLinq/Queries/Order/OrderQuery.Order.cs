namespace OptiLinq;

public partial struct OrderQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public OrderQuery<T, TBaseQuery, TBaseEnumerator, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, TOtherComparer>(ref _baseEnumerable, comparer);
	}

	public OrderQuery<T, TBaseQuery, TBaseEnumerator, Comparer<T>> Order()
	{
		return new OrderQuery<T, TBaseQuery, TBaseEnumerator, Comparer<T>>(ref _baseEnumerable, Comparer<T>.Default);
	}
}