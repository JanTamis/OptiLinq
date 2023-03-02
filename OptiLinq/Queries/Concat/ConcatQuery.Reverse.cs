namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public ReverseQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> Reverse()
	{
		return new ReverseQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(ref this);
	}
}