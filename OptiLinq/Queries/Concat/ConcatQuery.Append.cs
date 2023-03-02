namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public AppendQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> Append(in T element)
	{
		return new AppendQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(ref this, in element);
	}
}