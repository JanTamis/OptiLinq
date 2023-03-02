namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public OrderQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
	{
		return new OrderQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, TOtherComparer>(ref this, comparer);
	}

	public OrderQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, Comparer<T>> Order()
	{
		return new OrderQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}