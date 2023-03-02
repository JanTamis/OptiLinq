using OptiLinq.Helpers;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ExceptEnumerator<T, TComparer> : IOptiEnumerator<T>
	where TComparer : IEqualityComparer<T>
{
	private readonly IOptiEnumerator<T> _enumerator;

	private PooledSet<T, TComparer> _set;

	public ExceptEnumerator(IOptiEnumerator<T> enumerator, PooledSet<T, TComparer> set)
	{
		_enumerator = enumerator;
		_set = set;
	}

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		while (_enumerator.MoveNext())
		{
			if (_set.AddIfNotPresent(_enumerator.Current))
			{
				Current = _enumerator.Current;
				return true;
			}
		}

		return false;
	}

	public void Dispose()
	{
		_enumerator.Dispose();
	}
}