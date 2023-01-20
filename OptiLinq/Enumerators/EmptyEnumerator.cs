using OptiLinq.Interfaces;

namespace OptiLinq;

public struct EmptyEnumerator<T> : IOptiEnumerator<T>
{
	public T Current => default!;

	public bool MoveNext()
	{
		return false;
	}
	
	public void Dispose()
	{
		
	}
}