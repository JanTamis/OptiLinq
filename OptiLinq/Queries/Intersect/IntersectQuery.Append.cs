namespace OptiLinq;

public partial struct IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>
{
	public AppendQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>> Append(in T element)
	{
		return new AppendQuery<T, IntersectQuery<T, TComparer, TFirstQuery, TFirstEnumerator, TSecondQuery>, IntersectEnumerator<T, TFirstEnumerator, TComparer>>(ref this, in element);
	}
}