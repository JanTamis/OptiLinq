using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct IntersectEnumerator<T, TComparer> : IOptiEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private readonly IOptiEnumerator<T> _firstEnumerator;
	private readonly IOptiEnumerator<T> _secondEnumerator;

	private bool _isInitialized = false;

	private readonly int _capacity;

	private readonly TComparer _comparer;
	private PooledSet<T, TComparer> _set;

	public IntersectEnumerator(IOptiEnumerator<T> firstEnumerator, IOptiEnumerator<T> secondEnumerator, TComparer comparer, int capacity)
	{
		_firstEnumerator = firstEnumerator;
		_secondEnumerator = secondEnumerator;
		_comparer = comparer;
		_capacity = capacity;
	}

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		if (!_isInitialized)
		{
			_set = GetSet();
			_isInitialized = true;
		}

		while (_firstEnumerator.MoveNext())
		{
			if (_set.Remove(_firstEnumerator.Current))
			{
				Current = _firstEnumerator.Current;
				return true;
			}
		}

		return false;
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_secondEnumerator.Dispose();
	}

	private PooledSet<T, TComparer> GetSet()
	{
		var set = new PooledSet<T, TComparer>(Math.Max(_capacity, 4), _comparer);

		while (_secondEnumerator.MoveNext())
		{
			set.AddIfNotPresent(_secondEnumerator.Current);
		}

		return set;
	}
}