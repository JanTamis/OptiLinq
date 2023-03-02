namespace OptiLinq.Interfaces;

public interface IFunction<TIn, TIn2, out TOut>
{
	TOut Eval(in TIn element1, in TIn2 element2);
}

public interface IFunction<TIn, out TOut>
{
	TOut Eval(in TIn element);
}

public interface IFunction<out TOut>
{
	TOut Eval();
}