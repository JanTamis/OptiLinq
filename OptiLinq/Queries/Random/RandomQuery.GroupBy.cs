using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct RandomQuery
{
	public GroupByQuery<int, TKey, TKeySelector, RandomQuery, RandomEnumerator, TComparer> GroupBy<TKey, TKeySelector, TComparer>(TKeySelector keySelector = default, TComparer comparer = default)
		where TKeySelector : struct, IFunction<int, TKey>
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<int, TKey, TKeySelector, RandomQuery, RandomEnumerator, TComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<int, TKey, TKeySelector, RandomQuery, RandomEnumerator, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<int, TKey>
	{
		return new GroupByQuery<int, TKey, TKeySelector, RandomQuery, RandomEnumerator, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, EqualityComparer<TKey>> GroupBy<TKey>(Func<int, TKey> keySelector)
	{
		return new GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, EqualityComparer<TKey>>(this, new FuncAsIFunction<int, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, TComparer> GroupBy<TKey, TComparer>(Func<int, TKey> keySelector, TComparer comparer = default)
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, TComparer>(this, new FuncAsIFunction<int, TKey>(keySelector), comparer);
	}

	public GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, EqualityComparer<TKey>> GroupBy<TKey>(Func<int, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<int, TKey, FuncAsIFunction<int, TKey>, RandomQuery, RandomEnumerator, EqualityComparer<TKey>>(this, new FuncAsIFunction<int, TKey>(keySelector), comparer);
	}
}