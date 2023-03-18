using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public ExceptQuery<ArrayQuery<T>, EqualityComparer<ArrayQuery<T>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Except<TOtherQuery>(in TOtherQuery other) where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
	{
		return new ExceptQuery<ArrayQuery<T>, EqualityComparer<ArrayQuery<T>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(this, other, EqualityComparer<ArrayQuery<T>>.Default);
	}

	public ExceptQuery<ArrayQuery<T>, TOtherComparer, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Except<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
		where TOtherComparer : IEqualityComparer<ArrayQuery<T>>
	{
		return new ExceptQuery<ArrayQuery<T>, TOtherComparer, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(this, other, comparer);
	}
}