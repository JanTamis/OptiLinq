using System.Numerics;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TSecondQuery>
{
	public TakeLastQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>> TakeLast(int count)
	{
		return new TakeLastQuery<T, ConcatQuery<T, TFirstQuery, TSecondQuery>, ConcatEnumerator<T>>(this, count);
	}
}