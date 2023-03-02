namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>
{
	public AppendQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>> Append(in T element)
	{
		return new AppendQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TSecondQuery>, IntersectEnumerator<T, TComparer>>(ref this, in element);
	}
}