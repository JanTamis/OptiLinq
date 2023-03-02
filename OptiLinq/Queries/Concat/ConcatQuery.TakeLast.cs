using System.Numerics;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public TakeLastQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>> TakeLast(int count)
	{
		return new TakeLastQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(this, count);
	}
}