namespace OptiLinq.Interfaces;

public readonly struct FuncComparerWithPreviousDescending<T, TKey, TPrevious> : IOptiComparer<T>
	where TPrevious : IOptiComparer<T>
{
	private readonly Func<T, TKey> _keySelector;
	private readonly TPrevious _previous;

	public FuncComparerWithPreviousDescending(Func<T, TKey> keySelector, TPrevious previous)
	{
		_keySelector = keySelector;
		_previous = previous;
	}

	public int Compare(in T x, in T y)
	{
		var result = _previous.Compare(in x, in y);

		if (result == 0)
		{
			result = Comparer<TKey>.Default.Compare(_keySelector(x), _keySelector(y));
		}

		return -result;
	}
}

public readonly struct FuncComparerWithPreviousDescending<T, TKey, TKeyComparer, TPrevious> : IOptiComparer<T>
	where TKeyComparer : IComparer<TKey>
	where TPrevious : IOptiComparer<T>
{
	private readonly Func<T, TKey> _keySelector;
	private readonly TKeyComparer _keyComparer;
	private readonly TPrevious _previous;

	public FuncComparerWithPreviousDescending(Func<T, TKey> keySelector, TKeyComparer keyComparer, TPrevious previous)
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
			result = _keyComparer.Compare(_keySelector(x), _keySelector(y));
		}

		return -result;
	}
}