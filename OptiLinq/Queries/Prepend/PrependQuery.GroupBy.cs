using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct PrependQuery<T, TBaseQuery, TBaseEnumerator>
{
	public GroupByQuery<T, TKey, TKeySelector, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer> GroupBy<TKey, TKeySelector, TComparer>(TKeySelector keySelector = default, TComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TKey, TKeySelector, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer> GroupBy<TKey, TComparer>(Func<T, TKey> keySelector, TComparer comparer = default)
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, TComparer>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, PrependQuery<T, TBaseQuery, TBaseEnumerator>, PrependEnumerator<T, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}
}