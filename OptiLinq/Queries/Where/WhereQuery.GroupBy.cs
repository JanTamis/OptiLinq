using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public GroupByQuery<T, TKey, TKeySelector, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer> GroupBy<TKey, TKeySelector, TOtherComparer>(TKeySelector keySelector = default, TOtherComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TKey, TKeySelector, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer> GroupBy<TKey, TOtherComparer>(Func<T, TKey> keySelector, TOtherComparer comparer = default)
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, TOtherComparer>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, WhereQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, WhereEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}
}