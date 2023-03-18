using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>
{
	public IntersectQuery<ArrayQuery<T>, TOtherComparer, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Intersect<TOtherQuery, TOtherComparer>(in TOtherQuery other, TOtherComparer comparer)
		where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
		where TOtherComparer : IEqualityComparer<ArrayQuery<T>>
	{
		return new IntersectQuery<ArrayQuery<T>, TOtherComparer, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(this, other, comparer);
	}

	public IntersectQuery<ArrayQuery<T>, EqualityComparer<ArrayQuery<T>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery> Intersect<TOtherQuery>(in TOtherQuery other)
		where TOtherQuery : struct, IOptiQuery<ArrayQuery<T>>
	{
		return new IntersectQuery<ArrayQuery<T>, EqualityComparer<ArrayQuery<T>>, GroupByQuery<T, TKey, TKeySelector, TBaseQuery, TBaseEnumerator, TComparer>, GroupByEnumerator<TKey, T, TComparer>, TOtherQuery>(this, other, EqualityComparer<ArrayQuery<T>>.Default);
	}
}