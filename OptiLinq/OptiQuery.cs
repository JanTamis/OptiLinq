using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OptiLinq.Collections;
using OptiLinq.Interfaces;
using OptiLinq.Operators;

namespace OptiLinq;

public static class OptiQuery
{
	public static EnumerableQuery<T> AsOptiQuery<T>(this IEnumerable<T> enumerable)
	{
		return new EnumerableQuery<T>(enumerable);
	}

	public static ListQuery<T> AsOptiQuery<T>(this List<T> list)
	{
		return new ListQuery<T>(list);
	}

	public static ArrayQuery<T> AsOptiQuery<T>(this T[] list)
	{
		return new ArrayQuery<T>(list);
	}

	public static HashSetQuery<T> AsOptiQuery<T>(this HashSet<T> list)
	{
		return new HashSetQuery<T>(list);
	}

	public static RangeQuery Range(int start, int count)
	{
		return new RangeQuery(start, count);
	}

	public static RepeatQuery<T> Repeat<T>(T element)
	{
		return new RepeatQuery<T>(element);
	}

	public static EmptyQuery<T> Empty<T>()
	{
		return new EmptyQuery<T>();
	}

	public static GenerateQuery<T, TOperator> Generate<TOperator, T>(T initial = default, TOperator @operator = default) where TOperator : struct, IFunction<T, T>
	{
		return new GenerateQuery<T, TOperator>(initial, @operator);
	}

	public static GenerateQuery<T, FuncAsIFunction<T, T>> Generate<T>(Func<T, T> @operator, T initial = default)
	{
		return new GenerateQuery<T, FuncAsIFunction<T, T>>(initial, new FuncAsIFunction<T, T>(@operator));
	}

	public static RandomQuery Random(int? seed = null)
	{
		return new RandomQuery(seed);
	}

	public static SingletonQuery<T> Singleton<T>(in T element)
	{
		return new SingletonQuery<T>(in element);
	}

	public static bool SequenceEquals<T>(this IOptiQuery<T> firstQuery, IOptiQuery<T> secondQuery, IEqualityComparer<T>? comparer = default)
	{
		if (firstQuery.TryGetNonEnumeratedCount(out var firstCount) && secondQuery.TryGetNonEnumeratedCount(out var secondCount) && firstCount != secondCount)
		{
			return false;
		}

		comparer ??= EqualityComparer<T>.Default;

		using var firstEnumerator = firstQuery.GetEnumerator();
		using var secondEnumerator = secondQuery.GetEnumerator();

		while (firstEnumerator.MoveNext())
		{
			if (!(secondEnumerator.MoveNext() && comparer.Equals(firstEnumerator.Current, secondEnumerator.Current)))
			{
				return false;
			}
		}

		return !secondEnumerator.MoveNext();
	}

	public static TNumber Sum<TNumber>(this IOptiQuery<TNumber> query) where TNumber : struct, INumberBase<TNumber>
	{
		return Sum<TNumber, IOptiQuery<TNumber>>(query);
	}

	public static TNumber Sum<TNumber, TQuery>(this TQuery query)
		where TNumber : struct, INumberBase<TNumber>
		where TQuery : IOptiQuery<TNumber>
	{
		if (query.TryGetSpan(out var data))
		{
			return Sum(data);
		}

		return query.Aggregate(new SumOperator<TNumber>(), TNumber.Zero);
	}

	public static TNumber Sum<T, TNumber>(this IOptiQuery<T> query, Func<T, TNumber> selector) where TNumber : struct, INumberBase<TNumber>
	{
		return query.Aggregate(new SumOperator<T, TNumber>(selector), TNumber.Zero);
	}

	public static TNumber Sum<T, TNumber, TSelector>(this IOptiQuery<T> query, TSelector selector = default)
		where TNumber : struct, INumberBase<TNumber>
		where TSelector : struct, IFunction<T, TNumber>
	{
		return query.Aggregate(new SumOperator<T, TNumber, TSelector>(selector), TNumber.Zero);
	}

	public static double Average(this IOptiQuery<int> query) => query.Average<int, double>();
	public static double Average(this IOptiQuery<long> query) => query.Average<long, double>();
	public static float Average(this IOptiQuery<float> query) => query.Average<float, float>();
	public static double Average(this IOptiQuery<double> query) => query.Average<double, double>();
	public static decimal Average(this IOptiQuery<decimal> query) => query.Average<decimal, decimal>();

	internal static TResult Average<TNumber, TResult>(this IOptiQuery<TNumber> query)
		where TNumber : struct, INumberBase<TNumber>
		where TResult : struct, INumberBase<TResult>
	{
		if (query.TryGetSpan(out var data))
		{
			return TResult.CreateChecked(Sum(data)) / TResult.CreateChecked(data.Length);
		}

		var result = query.Aggregate(new AverageOperator<TResult, TNumber>(), (TResult.Zero, TResult.Zero));

		if (result.Item2 == TResult.Zero)
		{
			throw new InvalidOperationException("Sequence contains no elements");
		}

		return result.Item1 / result.Item2;
	}

	public static string ToString<T>(this IOptiQuery<T> query, string separator, string? format, IFormatProvider? provider = null)
		where T : ISpanFormattable
	{
		return EnumerableHelper.JoinFormattable<T, IOptiQuery<T>>(query, separator, format, provider);
	}

	public static TAccumulate Aggregate<T, TAccumulate>(this IOptiQuery<T> query, Func<TAccumulate, T, TAccumulate> @operator = default, TAccumulate seed = default)
	{
		return query.Aggregate(new FuncAsIFunction<TAccumulate, T, TAccumulate>(@operator), seed);
	}

	public static TResult Aggregate<T, TResult, TAccumulate>(this IOptiQuery<T> query, Func<TAccumulate, T, TAccumulate> @operator, Func<TAccumulate, TResult> resultSelector, TAccumulate seed = default)
	{
		return query.Aggregate<FuncAsIFunction<TAccumulate, T, TAccumulate>, FuncAsIFunction<TAccumulate, TResult>, TAccumulate, TResult>(new FuncAsIFunction<TAccumulate, T, TAccumulate>(@operator), new FuncAsIFunction<TAccumulate, TResult>(resultSelector), seed);
	}

	public static bool Any<T>(this IOptiQuery<T> query, Func<T, bool> @operator)
	{
		return query.Any(new FuncAsIFunction<T, bool>(@operator));
	}

	public static bool All<T>(this IOptiQuery<T> query, Func<T, bool> @operator)
	{
		return query.All(new FuncAsIFunction<T, bool>(@operator));
	}

	public static bool Contains<T>(this IOptiQuery<T> query, T element) where T : IEquatable<T>
	{
		if (query.TryGetSpan(out var data))
		{
			data.Contains(element);
		}

		return query.Contains(element);
	}

	public static void ForEach<TQuery, T>(this TQuery query, Action<T> action) where TQuery : IOptiQuery<T>
	{
		query.ForEach(new ActionAsIFunction<T>(action));
	}

	public static TNumber Count<T, TNumber>(this IOptiQuery<T> query, Func<T, bool> action) where TNumber : struct, INumberBase<TNumber>
	{
		return query.Count<FuncAsIFunction<T, bool>, TNumber>(new FuncAsIFunction<T, bool>(action));
	}

	internal static TNumber Sum<TNumber>(ReadOnlySpan<TNumber> data) where TNumber : struct, INumberBase<TNumber>
	{
		if (Vector.IsHardwareAccelerated && Vector<TNumber>.IsSupported)
		{
			ref var first = ref MemoryMarshal.GetReference(data);
			ref var last = ref Unsafe.Add(ref first, data.Length);

			var vectorSum = Unsafe.As<TNumber, Vector<TNumber>>(ref first);
			first = ref Unsafe.Add(ref first, Vector<TNumber>.Count);

			while (Unsafe.IsAddressLessThan(ref first, ref Unsafe.Subtract(ref last, Vector<TNumber>.Count)))
			{
				var vec1 = Unsafe.As<TNumber, Vector<TNumber>>(ref first);
				var vec2 = Unsafe.As<TNumber, Vector<TNumber>>(ref Unsafe.Add(ref first, Vector<TNumber>.Count));

				vectorSum += vec1 + vec2;
				first = ref Unsafe.Add(ref first, Vector<TNumber>.Count * 2);
			}

			while (Unsafe.IsAddressLessThan(ref first, ref last))
			{
				vectorSum += Unsafe.As<TNumber, Vector<TNumber>>(ref first);
				first = ref Unsafe.Add(ref first, Vector<TNumber>.Count);
			}

			return Vector.Sum(vectorSum);
		}

		var sum = TNumber.Zero;

		for (var i = 0; i < data.Length; i++)
		{
			sum += data[i];
		}

		return sum;
	}
}