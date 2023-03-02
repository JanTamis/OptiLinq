namespace OptiLinq.Interfaces;

public interface IAction<TIn>
{
	void Do(in TIn element);
}