using OptiLinq.Interfaces;

namespace OptiLinq;

public struct EnumerableEnumerator<T> : IOptiEnumerator<T>
{
	private IEnumerator<T> _enumerator;

	internal EnumerableEnumerator(IEnumerator<T> enumerator)
	{
		_enumerator = enumerator;
	}

	public T Current => _enumerator.Current;

	public bool MoveNext()
	{
		return _enumerator.MoveNext();
	}
	
	public void Dispose()
	{
		_enumerator.Dispose();
	}
}