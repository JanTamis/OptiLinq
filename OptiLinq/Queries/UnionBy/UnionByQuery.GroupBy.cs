using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>
{
	public GroupByQuery<T, TOtherKey, TOtherKeySelector, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, TOtherComparer> GroupBy<TOtherKey, TOtherKeySelector, TOtherComparer>(TOtherKeySelector keySelector = default, TOtherComparer comparer = default)
		where TOtherKeySelector : struct, IFunction<T, TOtherKey>
		where TOtherComparer : IEqualityComparer<TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, TOtherKeySelector, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, TOtherComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TOtherKey, TOtherKeySelector, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey, TOtherKeySelector>(TOtherKeySelector keySelector)
		where TOtherKeySelector : struct, IFunction<T, TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, TOtherKeySelector, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>>(this, keySelector, EqualityComparer<TOtherKey>.Default);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey>(Func<T, TOtherKey> keySelector)
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), EqualityComparer<TOtherKey>.Default);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, TOtherComparer> GroupBy<TOtherKey, TOtherComparer>(Func<T, TOtherKey> keySelector, TOtherComparer comparer = default)
		where TOtherComparer : IEqualityComparer<TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, TOtherComparer>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey>(Func<T, TOtherKey> keySelector, EqualityComparer<TOtherKey> comparer)
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, UnionByQuery<T, TKey, TFirstQuery, TFirstEnumerator, TSecondQuery, TKeySelector, TComparer>, UnionByEnumerator<T, TKey, TFirstEnumerator, IEnumerator<T>, TKeySelector, TComparer>, EqualityComparer<TOtherKey>>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), comparer);
	}
}