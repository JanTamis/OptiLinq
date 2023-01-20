namespace OptiLinq.Interfaces;

public interface IWhereOperator<in T>
{
	static abstract bool IsAccepted(T item);
}