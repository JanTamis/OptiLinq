using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>
{
	public GroupByQuery<T, TKey, TKeySelector, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer> GroupBy<TKey, TKeySelector, TOtherComparer>(TKeySelector keySelector = default, TOtherComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TKey, TKeySelector, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer> GroupBy<TKey, TOtherComparer>(Func<T, TKey> keySelector, TOtherComparer comparer = default)
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, TOtherComparer>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, DistinctQuery<T, TBaseQuery, TBaseEnumerator, TComparer>, DistinctEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}
}