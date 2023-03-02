namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public DefaultIfEmptyQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> DefaultIfEmpty(in ArrayQuery<T> defaultValue = default)
	{
		return new DefaultIfEmptyQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this, defaultValue);
	}
}