namespace OptiLinq.Interfaces;

public interface IOptiQuery<T, out TEnumerator> where TEnumerator : struct, IOptiEnumerator<T>
{
	bool Any();

	bool Contains(T item, IEqualityComparer<T>? comparer);
	int Count();

	T First();
	T FirstOrDefault();

	bool TryGetNonEnumeratedCount(out int length);
	
	TEnumerator GetEnumerator();
}