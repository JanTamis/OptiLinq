using System.Numerics;

namespace OptiLinq;

public partial struct EnumerableQuery<T>
{
	public TakeLastQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, EnumerableQuery<T>, EnumerableEnumerator<T>>(this, count);
	}
}