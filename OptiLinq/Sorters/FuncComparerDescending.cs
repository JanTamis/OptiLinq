namespace OptiLinq.Interfaces;

public readonly struct FuncComparerDescending<T, TKey> : IOptiComparer<T>
{
	private readonly Func<T, TKey> _keySelector;

	public FuncComparerDescending(Func<T, TKey> keySelector)
	{
		_keySelector = keySelector;
	}

	public int Compare(in T x, in T y)
	{
		return -Comparer<TKey>.Default.Compare(_keySelector(x), _keySelector(y));
	}
}

public readonly struct FuncComparerDescending<T, TKey, TKeyComparer> : IOptiComparer<T>
	where TKeyComparer : IComparer<TKey>
{
	private readonly Func<T, TKey> _keySelector;
	private readonly TKeyComparer _keyComparer;

	public FuncComparerDescending(Func<T, TKey> keySelector, TKeyComparer keyComparer)
	{
		_keySelector = keySelector;
		_keyComparer = keyComparer;
	}

	public int Compare(in T x, in T y)
	{
		return -_keyComparer.Compare(_keySelector(x), _keySelector(y));
	}
}