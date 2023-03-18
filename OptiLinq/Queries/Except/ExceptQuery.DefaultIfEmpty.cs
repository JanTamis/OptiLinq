namespace OptiLinq;

public partial struct ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public DefaultIfEmptyQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>> DefaultIfEmpty(in T defaultValue = default)
	{
		return new DefaultIfEmptyQuery<T, ExceptQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, ExceptEnumerator<T, TFirstEnumerator, TComparer>>(ref this, defaultValue);
	}
}