namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ReverseQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>> Reverse()
	{
		return new ReverseQuery<T, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>>(ref this);
	}
}