using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public GroupByQuery<T, TKey, TKeySelector, ArrayQuery<T>, ArrayEnumerator<T>, TComparer> GroupBy<TKey, TKeySelector, TComparer>(TKeySelector keySelector = default, TComparer comparer = default)
		where TKeySelector : struct, IFunction<T, TKey>
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, ArrayQuery<T>, ArrayEnumerator<T>, TComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<T, TKey, TKeySelector, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<T, TKey>
	{
		return new GroupByQuery<T, TKey, TKeySelector, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, TComparer> GroupBy<TKey, TComparer>(Func<T, TKey> keySelector, TComparer comparer = default)
		where TComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, TComparer>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}

	public GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>> GroupBy<TKey>(Func<T, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<T, TKey, FuncAsIFunction<T, TKey>, ArrayQuery<T>, ArrayEnumerator<T>, EqualityComparer<TKey>>(this, new FuncAsIFunction<T, TKey>(keySelector), comparer);
	}
}