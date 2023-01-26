using OptiLinq.Interfaces;

namespace OptiLinq;

public static class OptiQuery
{
	public static EnumerableQuery<T> AsOptiQuery<T>(this IEnumerable<T> enumerable) where T : IEquatable<T>
	{
		return new EnumerableQuery<T>(enumerable);
	}

	public static ListQuery<T> AsOptiQuery<T>(this IList<T> list) where T : IEquatable<T>
	{
		return new ListQuery<T>(list);
	}

	public static RangeQuery Range(int start, int count)
	{
		return new RangeQuery(start, count);
	}

	public static RepeatQuery<T> Repeat<T>(T element, int count)
	{
		return new RepeatQuery<T>(element, count);
	}

	public static EmptyQuery<T> Empty<T>()
	{
		return new EmptyQuery<T>();
	}

	public static GenerateQuery<T, TOperator> Generate<T, TOperator>(T initial) where TOperator : IFunction<T, T>
	{
		return new GenerateQuery<T, TOperator>(initial);
	}
}