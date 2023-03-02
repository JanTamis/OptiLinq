using OptiLinq.Interfaces;

namespace OptiLinq;

public struct SingletonEnumerator<T> : IOptiEnumerator<T>
{
	private bool _isValid = true;

	internal SingletonEnumerator(T element)
	{
		Current = element;
	}

	public T Current { get; }

	public bool MoveNext()
	{
		if (_isValid)
		{
			_isValid = false;
			return true;
		}

		return false;
	}

	public void Dispose()
	{
	}
}