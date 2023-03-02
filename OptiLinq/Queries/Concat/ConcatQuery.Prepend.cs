namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public PrependQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> Prepend(in T item)
	{
		return new PrependQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(ref this, in item);
	}
}