using OptiLinq.Interfaces;

namespace OptiLinq;

public struct DefaultIfEmptyEnumerator<T, TEnumerator> : IOptiEnumerator<T> where TEnumerator : struct, IOptiEnumerator<T>
{
	private TEnumerator _enumerator;
	private readonly T _defaultValue;

	private int _state = 1;

	internal DefaultIfEmptyEnumerator(TEnumerator enumerator, T defaultValue)
	{
		_enumerator = enumerator;
		_defaultValue = defaultValue;
	}

	public T Current { get; private set; } = default!;

	public bool MoveNext()
	{
		switch (_state)
		{
			case 1:
				if (_enumerator.MoveNext())
				{
					Current = _enumerator.Current;
					_state = 2;
				}
				else
				{
					Current = _defaultValue;
					_state = -1;
				}

				return true;
			case 2:
				if (_enumerator.MoveNext())
				{
					Current = _enumerator.Current;
					return true;
				}

				break;
		}

		return false;
	}

	public void Dispose()
	{
		_enumerator.Dispose();
	}
}