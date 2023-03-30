namespace OptiLinq.Interfaces;

public readonly struct IFuncComparer<T, TKey, TKeySelector> : IOptiComparer<T>
	where TKeySelector : IFunction<T, TKey>
{
	private readonly TKeySelector _keySelector;

	public IFuncComparer(TKeySelector keySelector)
	{
		_keySelector = keySelector;
	}

	public int Compare(in T x, in T y)
	{
		return Comparer<TKey>.Default.Compare(_keySelector.Eval(in x), _keySelector.Eval(in y));
	}
}

public readonly struct IFuncComparer<T, TKey, TKeySelector, TKeyComparer> : IOptiComparer<T>
	where TKeySelector : IFunction<T, TKey>
	where TKeyComparer : IComparer<TKey>
{
	private readonly TKeySelector _keySelector;
	private readonly TKeyComparer _keyComparer;

	public IFuncComparer(TKeySelector keySelector, TKeyComparer keyComparer)
	{
		_keySelector = keySelector;
		_keyComparer = keyComparer;
	}

	public int Compare(in T x, in T y)
	{
		return _keyComparer.Compare(_keySelector.Eval(in x), _keySelector.Eval(in y));
	}
}