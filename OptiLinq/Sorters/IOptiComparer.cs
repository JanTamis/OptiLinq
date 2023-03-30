namespace OptiLinq.Interfaces;

public interface IOptiComparer<T>
{
	int Compare(in T x, in T y);
}