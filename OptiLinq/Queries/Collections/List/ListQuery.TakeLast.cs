using System.Numerics;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public TakeLastQuery<T, ListQuery<T>, ListEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, ListQuery<T>, ListEnumerator<T>>(this, count);
	}
}