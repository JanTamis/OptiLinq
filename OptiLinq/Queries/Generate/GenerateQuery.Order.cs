namespace OptiLinq;

public partial struct GenerateQuery<T, TOperator>
{
	public OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, Comparer<T>> Order()
	{
		return new OrderQuery<T, GenerateQuery<T, TOperator>, GenerateEnumerator<T, TOperator>, Comparer<T>>(ref this, Comparer<T>.Default);
	}
}