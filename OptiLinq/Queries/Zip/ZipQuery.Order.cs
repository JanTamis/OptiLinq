// using OptiLinq.Collections;
// using OptiLinq.Interfaces;
//
// namespace OptiLinq;
//
// public partial struct ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>
// {
// 	public OrderQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherComparer> Order<TOtherComparer>(TOtherComparer comparer) where TOtherComparer : IComparer<TResult>
// 	{
// 		return new OrderQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, TOtherComparer>(ref this, new EnumerableSorter<TResult, TOtherComparer>(comparer, false, null));
// 	}
//
// 	public OrderQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, Comparer<TResult>> Order()
// 	{
// 		return new OrderQuery<TResult, ZipQuery<T, TResult, TOperator, TFirstQuery, TFirstEnumerator, TSecondQuery>, ZipEnumerator<T, TResult, TOperator, TFirstEnumerator, IEnumerator<T>>, Comparer<TResult>>(ref this, new EnumerableSorter<TResult, Comparer<TResult>>(Comparer<TResult>.Default, false, null));
// 	}
// }

