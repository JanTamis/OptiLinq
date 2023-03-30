// using System.ComponentModel.Design.Serialization;
// using OptiLinq.Collections;
// using OptiLinq.Interfaces;
//
// namespace OptiLinq;
//
// public partial struct AppendQuery<T, TBaseQuery, TBaseEnumerator>
// {
// 	public OrderQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>, IdentitySorter<T, TComparer>> Order<TComparer>(TComparer comparer) where TComparer : IComparer<T>
// 	{
// 		return new OrderQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>, IdentitySorter<T, TComparer>>(ref this, new IdentitySorter<T, TComparer>(comparer));
// 	}
//
// 	public OrderQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>, IdentitySorter<T>> Order()
// 	{
// 		return new OrderQuery<T, AppendQuery<T, TBaseQuery, TBaseEnumerator>, AppendEnumerator<T, TBaseEnumerator>, IdentitySorter<T>>(ref this, new IdentitySorter<T, TComparer>(comparer));
// 	}
// }

