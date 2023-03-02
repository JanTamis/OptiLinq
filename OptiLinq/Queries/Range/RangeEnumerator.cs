using OptiLinq.Interfaces;

namespace OptiLinq;

public struct RangeEnumerator : IOptiEnumerator<int>
{
	private readonly int _end;
	private int _current = 0;

	public RangeEnumerator(int start, int count)
	{
		_end = start + count;
	}

	public int Current => _current;

	public bool MoveNext()
	{
		if (_current + 1 <= _end)
		{
			_current++;
			return true;
		}

		return false;
	}

	public void Dispose()
	{

	}
}