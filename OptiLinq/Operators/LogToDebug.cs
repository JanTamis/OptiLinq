using System.Diagnostics;
using OptiLinq.Interfaces;

namespace OptiLinq.Operators;

public readonly struct LogToDebug<T> : IAction<T>
{
	public void Do(in T element)
	{
		Debug.WriteLine(element?.ToString());
	}
}