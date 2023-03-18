using System.Collections;

namespace OptiLinq;

public struct SingletonEnumerator<T> : IEnumerator<T>
{
	private bool _isValid = true;

	internal SingletonEnumerator(T element)
	{
		Current = element;
	}

	object IEnumerator.Current => Current;

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

	public void Reset()
	{
		_isValid = true;
	}

	public void Dispose()
	{
	}
}