namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<T>
	{
		return new OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, TOtherComparer>(ref this, comparer);
	}

	public OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, Comparer<T>> Order()
	{
		return new OrderQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}