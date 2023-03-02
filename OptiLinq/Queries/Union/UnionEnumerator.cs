using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct UnionEnumerator<T, TComparer> : IOptiEnumerator<T> where TComparer : IEqualityComparer<T>
{
	private IOptiEnumerator<T> _firstEnumerator;

	private PooledSet<T, TComparer> _set;

	internal UnionEnumerator(IOptiEnumerator<T> firstEnumerator, IOptiEnumerator<T> secondEnumerator, TComparer comparer, int capacity)
	{
		_firstEnumerator = firstEnumerator;
		_set = new PooledSet<T, TComparer>(capacity, comparer);

		while (secondEnumerator.MoveNext())
		{
			_set.AddIfNotPresent(secondEnumerator.Current);
		}

		secondEnumerator.Dispose();
	}

	public T Current { get; private set; }

	public bool MoveNext()
	{
		while (_firstEnumerator.MoveNext())
		{
			if (_set.AddIfNotPresent(_firstEnumerator.Current))
			{
				Current = _firstEnumerator.Current;
				return true;
			}
		}

		Current = default!;
		return false;
	}

	public void Dispose()
	{
		_firstEnumerator.Dispose();
		_set.Dispose();
	}
}