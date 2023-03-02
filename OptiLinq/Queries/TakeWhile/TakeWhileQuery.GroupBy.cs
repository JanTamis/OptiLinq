using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>
{
	public GroupByQuery<T, TKey, TKeySelector, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer> GroupBy<TKey, TKeySelector, TComparer>(TKeySelector keySelector = default, TComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TKey, TKeySelector, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer> GroupBy<TKey, TComparer>(Func<T, TKey> keySelector, TComparer comparer = default)
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, TComparer>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, TakeWhileQuery<T, TOperator, TBaseQuery, TBaseEnumerator>, TakeWhileEnumerator<T, TOperator, TBaseEnumerator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}
}