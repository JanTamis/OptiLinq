namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ReverseQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>> Reverse()
	{
		return new ReverseQuery<ArrayQuery<T>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>>(ref this);
	}
}