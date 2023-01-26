using OptiLinq.Interfaces;

namespace OptiLinq;

public struct ConcatEnumerator<T> : IOptiEnumerator<T>
{
	public T Current { get; }
	
	public bool MoveNext()
	{
		throw new NotImplementedException();
	}

	public void Dispose()
	{
		throw new NotImplementedException();
	}
}