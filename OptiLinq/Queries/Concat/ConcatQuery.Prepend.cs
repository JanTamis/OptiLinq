using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public PrependQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>> Prepend(in T item)
	{
		return new PrependQuery<T, ConcatQuery<T, TFirstQuery, TFirstEnumerator, TSecondQuery>, ConcatEnumerator<T, TFirstEnumerator, IOptiEnumerator<T>>>(ref this, in item);
	}
}