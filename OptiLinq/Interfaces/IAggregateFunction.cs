namespace OptiLinq.Interfaces;

public interface IAggregateFunction<TIn, TIn2, out TOut>
{
	TOut Eval(in TIn accumulator, in TIn2 element);
}