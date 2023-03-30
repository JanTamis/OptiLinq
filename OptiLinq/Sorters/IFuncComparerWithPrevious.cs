namespace OptiLinq.Interfaces;

public readonly struct IFuncComparerWithPrevious<T, TKey, TKeySelector, TPrevious> : IOptiComparer<T>
	where TKeySelector : IFunction<T, TKey>
	where TPrevious : IOptiComparer<T>
{
	private readonly TKeySelector _keySelector;
	private readonly TPrevious _previous;

	public IFuncComparerWithPrevious(TKeySelector keySelector, TPrevious previous)
	{
		_keySelector = keySelector;
		_previous = previous;
	}

	public int Compare(in T x, in T y)
	{
		var result = _previous.Compare(in x, in y);

		if (result == 0)
		{
			result = Comparer<TKey>.Default.Compare(_keySelector.Eval(in x), _keySelector.Eval(in y));
		}

		return result;
	}
}

public readonly struct IFuncComparerWithPrevious<T, TKey, TKeySelector, TKeyComparer, TPrevious> : IOptiComparer<T>
	where TKeySelector : IFunction<T, TKey>
	where TKeyComparer : IComparer<TKey>
	where TPrevious : IOptiComparer<T>
{
	private readonly TKeySelector _keySelector;
	private readonly TKeyComparer _keyComparer;
	private readonly TPrevious _previous;

	public IFuncComparerWithPrevious(TKeySelector keySelector, TKeyComparer keyComparer, TPrevious previous)
	{
		_keySelector = keySelector;
		_keyComparer = keyComparer;
		_previous = previous;
	}

	public int Compare(in T x, in T y)
	{
		var result = _previous.Compare(in x, in y);

		if (result == 0)
		{
			result = _keyComparer.Compare(_keySelector.Eval(in x), _keySelector.Eval(in y));
		}

		return result;
	}
}