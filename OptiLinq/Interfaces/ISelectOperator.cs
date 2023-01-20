namespace OptiLinq.Interfaces;

public interface ISelectOperator<in T, out TResult>
{
	static abstract TResult Transform(T item);
}