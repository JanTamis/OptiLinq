using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct HashSetEnumerator<T> : IOptiEnumerator<T>
{
	private HashSet<T>.Enumerator _enumerator;

	internal HashSetEnumerator(HashSet<T>.Enumerator enumerator)
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