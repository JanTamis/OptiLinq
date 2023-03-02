using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public ReverseQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>> Reverse()
	{
		return new ReverseQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IOptiEnumerator<T>>>(ref this);
	}
}