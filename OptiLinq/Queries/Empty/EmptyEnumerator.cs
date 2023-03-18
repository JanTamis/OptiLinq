using System.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public struct EmptyEnumerator<T> : IEnumerator<T>
{
	object IEnumerator.Current => Current;

	public T Current => default!;

	public bool MoveNext()
	{
		return false;
	}

	public void Reset()
	{
	}
	
	public void Dispose()
	{
		
	}
}