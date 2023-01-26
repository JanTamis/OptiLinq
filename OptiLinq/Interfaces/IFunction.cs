namespace OptiLinq.Interfaces;

public interface IFunction<in TIn, out TOut>
{
	static abstract TOut Eval(TIn element);
}