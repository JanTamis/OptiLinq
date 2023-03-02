using System.Numerics;

namespace OptiLinq;

public partial struct HashSetQuery<T>
{
	public TakeLastQuery<T, HashSetQuery<T>, HashSetEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, HashSetQuery<T>, HashSetEnumerator<T>>(this, count);
	}
}