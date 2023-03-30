using System.Collections;

namespace OptiLinq;

public struct RangeEnumerator : IEnumerator<int>
{
	private readonly int _start;
	private readonly int _end;
	private int _current = 0;

	public RangeEnumerator(int start, int count)
	{
		_start = start;
		_end = start + count;
	}

	object IEnumerator.Current => Current;

	public int Current => _current;

	public bool MoveNext()
	{
		var current = _current + 1;

		if (current <= _end)
		{
			_current = current;
			return true;
		}

		return false;
	}

	public void Reset()
	{
		_current = _start;
	}

	public void Dispose()
	{

	}
}