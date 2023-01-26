using System.Numerics;

namespace OptiLinq.Interfaces;

public interface IOptiQuery<T, out TEnumerator> where TEnumerator : struct, IOptiEnumerator<T>
{
	bool All<TAllOperator>() where TAllOperator : IFunction<T, bool>;
	bool Any();

	IEnumerable<T> AsEnumerable();

	bool Contains(T item, IEqualityComparer<T>? comparer);
	int Count();

	T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>;
	T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>;

	T First();
	T FirstOrDefault();

	T Last();
	T LastOrDefault();

	T Max();
	T Min();

	T Single();
	T SingleOrDefault();

	T[] ToArray();
	List<T> ToList();

	bool TryGetNonEnumeratedCount(out int length);
	
	TEnumerator GetEnumerator();
}