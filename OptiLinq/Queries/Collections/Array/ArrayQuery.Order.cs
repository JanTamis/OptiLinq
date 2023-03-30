using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ArrayQuery<T>
{
	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TSorter> Order<TSorter>(TSorter comparer = default) where TSorter : IOptiComparer<T>
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, TSorter>(ref this, comparer);
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparer<T>> Order()
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparer<T>>(ref this, new IdentityComparer<T>());
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IFuncComparer<T, TKey, TKeySelector>> OrderBy<TKey, TKeySelector>(TKeySelector keySelector = default) where TKeySelector : IFunction<T, TKey>
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IFuncComparer<T, TKey, TKeySelector>>(ref this, new IFuncComparer<T, TKey, TKeySelector>(keySelector));
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, FuncComparer<T, TKey>> OrderBy<TKey>(Func<T, TKey> keySelector = default)
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, FuncComparer<T, TKey>>(ref this, new FuncComparer<T, TKey>(keySelector));
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparerDescending<T, TComparer>> OrderDescending<TComparer>(TComparer comparer = default) where TComparer : IComparer<T>
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparerDescending<T, TComparer>>(ref this, new IdentityComparerDescending<T, TComparer>(comparer));
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparerDescending<T>> OrderDescending()
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IdentityComparerDescending<T>>(ref this, new IdentityComparerDescending<T>());
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IFuncComparerDescending<T, TKey, TKeySelector>> OrderByDescending<TKey, TKeySelector>(TKeySelector keySelector = default) where TKeySelector : IFunction<T, TKey>
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, IFuncComparerDescending<T, TKey, TKeySelector>>(ref this, new IFuncComparerDescending<T, TKey, TKeySelector>(keySelector));
	}

	public OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, FuncComparerDescending<T, TKey>> OrderDescending<TKey>(Func<T, TKey> keySelector = default)
	{
		return new OrderQuery<T, ArrayQuery<T>, ArrayEnumerator<T>, FuncComparerDescending<T, TKey>>(ref this, new FuncComparerDescending<T, TKey>(keySelector));
	}
}