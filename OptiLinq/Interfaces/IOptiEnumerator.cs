namespace OptiLinq.Interfaces;

public interface IOptiEnumerator<out T> : IDisposable
{
	T Current { get; }

	bool MoveNext();
}