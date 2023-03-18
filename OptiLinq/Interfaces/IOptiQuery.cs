using System.Numerics;
using OptiLinq.Collections;

namespace OptiLinq.Interfaces;

public interface IOptiQuery<T> : IEnumerable<T>
{
	TResult Aggregate<TFunc, TResultSelector, TAccumulate, TResult>(TFunc func = default, TResultSelector selector = default, TAccumulate seed = default)
		where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>
		where TResultSelector : struct, IFunction<TAccumulate, TResult>;

	TAccumulate Aggregate<TFunc, TAccumulate>(TFunc @operator = default, TAccumulate seed = default) where TFunc : struct, IAggregateFunction<TAccumulate, T, TAccumulate>;

	bool All<TAllOperator>(TAllOperator @operator = default) where TAllOperator : struct, IFunction<T, bool>;

	bool Any();
	bool Any<TAnyOperator>(TAnyOperator @operator = default) where TAnyOperator : struct, IFunction<T, bool>;

	IEnumerable<T> AsEnumerable();

	bool Contains<TComparer>(in T item, TComparer comparer) where TComparer : IEqualityComparer<T>;
	bool Contains(in T item);

	int CopyTo(Span<T> data);

	TNumber Count<TNumber>() where TNumber : INumberBase<TNumber>;
	int Count();
	long LongCount();

	TNumber Count<TCountOperator, TNumber>(TCountOperator @operator = default)
		where TNumber : INumberBase<TNumber>
		where TCountOperator : struct, IFunction<T, bool>;

	TNumber Count<TNumber>(Func<T, bool> predicate) where TNumber : INumberBase<TNumber>;
	int Count(Func<T, bool> predicate);
	long CountLong(Func<T, bool> predicate);

	bool TryGetElementAt<TIndex>(TIndex index, out T item) where TIndex : IBinaryInteger<TIndex>;
	T ElementAt<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>;
	T ElementAtOrDefault<TIndex>(TIndex index) where TIndex : IBinaryInteger<TIndex>;

	bool TryGetFirst(out T item);
	T First();
	T FirstOrDefault();

	Task ForAll<TAction>(TAction @operator = default, CancellationToken token = default) where TAction : struct, IAction<T>;
	void ForEach<TAction>(TAction @operator = default) where TAction : struct, IAction<T>;

	bool TryGetLast(out T item);
	T Last();
	T LastOrDefault();

	T Max();
	T Min();

	bool TryGetSingle(out T item);
	T Single();
	T SingleOrDefault();

	T[] ToArray();
	T[] ToArray(out int length);

	HashSet<T> ToHashSet(IEqualityComparer<T>? comparer = default);

	List<T> ToList();

	PooledList<T> ToPooledList();
	PooledQueue<T> ToPooledQueue();
	PooledStack<T> ToPooledStack();
	PooledSet<T, TComparer> ToPooledSet<TComparer>(TComparer comparer) where TComparer : IEqualityComparer<T>;

	bool TryGetNonEnumeratedCount(out int length);

	bool TryGetSpan(out ReadOnlySpan<T> span);
}

public interface IOptiQuery<T, out TEnumerator> : IOptiQuery<T> where TEnumerator : IEnumerator<T>
{
	new TEnumerator GetEnumerator();
}