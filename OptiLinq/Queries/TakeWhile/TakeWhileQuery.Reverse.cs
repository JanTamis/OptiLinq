namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public ReverseQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>> Reverse()
	{
		return new ReverseQuery<T, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>>(ref this);
	}
}