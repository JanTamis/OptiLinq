using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public GroupByQuery<T, TOtherKey, TOtherKeySelector, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer> GroupBy<TOtherKey, TOtherKeySelector, TOtherComparer>(TOtherKeySelector keySelector = default, TOtherComparer comparer = default)
		where TOtherKeySelector : struct, IFunction<T, TOtherKey>
		where TOtherComparer : IEqualityComparer<TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, TOtherKeySelector, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TOtherKey, TOtherKeySelector, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey, TOtherKeySelector>(TOtherKeySelector keySelector)
		where TOtherKeySelector : struct, IFunction<T, TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, TOtherKeySelector, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>>(this, keySelector, EqualityComparer<TOtherKey>.Default);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey>(Func<T, TOtherKey> keySelector)
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), EqualityComparer<TOtherKey>.Default);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer> GroupBy<TOtherKey, TOtherComparer>(Func<T, TOtherKey> keySelector, TOtherComparer comparer = default)
		where TOtherComparer : IEqualityComparer<TOtherKey>
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, TOtherComparer>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>> GroupBy<TOtherKey>(Func<T, TOtherKey> keySelector, EqualityComparer<TOtherKey> comparer)
	{
		return new GroupByQuery<T, TOtherKey, FuncAsIFunction<T, TOtherKey>, IntersectByQuery<T, TKey, TKeySelector, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectByEnumerator<T, TKey, TFirstEnumerator, TComparer, TKeySelector>, EqualityComparer<TOtherKey>>(this, new FuncAsIFunction<T, TOtherKey>(keySelector), comparer);
	}
}