namespace OptiLinq;

public static class OptiQuery
{
	public static EnumerableQuery<T> AsOptiQuery<T>(this IEnumerable<T> enumerable) where T : IEquatable<T>
	{
		return new EnumerableQuery<T>(enumerable);
	}

	public static RangeQuery Range(int start, int count)
	{
		return new RangeQuery(start, count);
	}

	public static EmptyQuery<T> Empty<T>()
	{
		return new EmptyQuery<T>();
	}
}