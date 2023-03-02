namespace OptiLinq.Interfaces;

public interface IOptiCollection<T, out TEnumerator> : IOptiCollection<T>, IOptiQuery<T, TEnumerator> where TEnumerator : struct, IOptiEnumerator<T>
{
}

public interface IOptiCollection<T> : IOptiQuery<T>
{
	T Get(int i);

	ref T GetUnsafe(int i);
}