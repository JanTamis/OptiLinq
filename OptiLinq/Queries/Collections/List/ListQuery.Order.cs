using OptiLinq.Collections;
using OptiLinq.Interfaces;

namespace OptiLinq;

public partial struct ListQuery<T>
{
	public OrderQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer> Order<TComparer>(TComparer comparer) where TComparer : IOptiComparer<T>
	{
		return new OrderQuery<T, ListQuery<T>, List<T>.Enumerator, TComparer>(ref this, comparer);
	}

	public OrderQuery<T, ListQuery<T>, List<T>.Enumerator, IdentityComparer<T>> Order()
	{
		return new OrderQuery<T, ListQuery<T>, List<T>.Enumerator, IdentityComparer<T>>(ref this, new IdentityComparer<T>());
	}
}

