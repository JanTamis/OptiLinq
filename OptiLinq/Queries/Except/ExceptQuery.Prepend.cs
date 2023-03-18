namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public PrependQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>> Prepend(in T item)
	{
		return new PrependQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(ref this, in item);
	}
}