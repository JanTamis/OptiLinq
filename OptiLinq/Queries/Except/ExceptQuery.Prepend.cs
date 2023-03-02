namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public PrependQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>> Prepend(in T item)
	{
		return new PrependQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TSecondQuery>, ExceptEnumerator<T, TComparer>>(ref this, in item);
	}
}