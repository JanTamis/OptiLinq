using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>
{
	public GroupByQuery<TResult, TKey, TKeySelector, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer> GroupBy<TKey, TKeySelector, TOtherComparer>(TKeySelector keySelector = default, TOtherComparer comparer = default)
		where TKeySelector : struct, IFunction<TResult, TKey>
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<TResult, TKey, TKeySelector, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer>(this, keySelector, comparer);
	}

	public GroupByQuery<TResult, TKey, TKeySelector, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>> GroupBy<TKey, TKeySelector>(TKeySelector keySelector)
		where TKeySelector : struct, IFunction<TResult, TKey>
	{
		return new GroupByQuery<TResult, TKey, TKeySelector, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>>(this, keySelector, EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<TResult, TKey> keySelector)
	{
		return new GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<TResult, TKey>(keySelector), EqualityComparer<TKey>.Default);
	}

	public GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer> GroupBy<TKey, TOtherComparer>(Func<TResult, TKey> keySelector, TOtherComparer comparer = default)
		where TOtherComparer : IEqualityComparer<TKey>
	{
		return new GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, TOtherComparer>(this, new FuncAsIFunction<TResult, TKey>(keySelector), comparer);
	}

	public GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>> GroupBy<TKey>(Func<TResult, TKey> keySelector, EqualityComparer<TKey> comparer)
	{
		return new GroupByQuery<TResult, TKey, FuncAsIFunction<TResult, TKey>, ZipQuery<T, TResult, TOperator, TFirstQuery, TSecondQuery>, ZipEnumerator<T, TResult, TOperator>, EqualityComparer<TKey>>(this, new FuncAsIFunction<TResult, TKey>(keySelector), comparer);
	}
}