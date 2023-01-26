using OptiLinq.Interfaces;

namespace OptiLinq;

public struct GenerateEnumerator<T, TOperator> : IOptiEnumerator<T> where TOperator : IFunction<T, T>
{
	internal GenerateEnumerator(T current)
	{
		Current = current;
	}

	public T Current { get; private set; }

	public bool MoveNext()
	{
		Current = TOperator.Eval(Current);
		return true;
	}
	
	public void Dispose()
	{
		
	}
}