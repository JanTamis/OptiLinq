using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct LogToConsole<T> : IAction<T>
{
	public void Do(in T element)
	{
		Console.WriteLine(element?.ToString());
	}
}