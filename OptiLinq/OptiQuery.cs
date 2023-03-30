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

	public static IListQuery<T> AsOptiQuery<T>(this IList<T> list)
	{
		return new IListQuery<T>(list);
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

		return query.Aggregate(TNumber.Zero, new SumOperator<TNumber>());
	}

	public static TNumber Sum<T, TNumber>(this IOptiQuery<T> query, Func<T, TNumber> selector) where TNumber : struct, INumberBase<TNumber>
	{
		return query.Aggregate(TNumber.Zero, new SumOperator<T, TNumber>(selector));
	}

	public static TNumber Sum<T, TNumber, TSelector>(this IOptiQuery<T> query, TSelector selector = default)
		where TNumber : struct, INumberBase<TNumber>
		where TSelector : struct, IFunction<T, TNumber>
	{
		return query.Aggregate(TNumber.Zero, new SumOperator<T, TNumber, TSelector>(selector));
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
		if (query.TryGetNonEnumeratedCount(out var count))
		{
			return TResult.CreateChecked(query.Sum()) / TResult.CreateChecked(count);
		}

		var result = query.Aggregate((TResult.Zero, TResult.Zero), new AverageOperator<TResult, TNumber>());

		if (result.Item2 == TResult.Zero)
		{
			throw new InvalidOperationException("Sequence contains no elements");
		}
		
		return result.Item1 / result.Item2;
	}

	public static string ToString<T>(this IEnumerable<T> query, string separator, string? format = null, IFormatProvider? provider = null)
		where T : ISpanFormattable
	{
		return EnumerableHelper.JoinFormattable<T, IEnumerable<T>>(query, separator, format, provider);
	}

	public static TNumber Sum<TNumber>(ReadOnlySpan<TNumber> data) where TNumber : struct, INumberBase<TNumber>
	{
		ref var first = ref MemoryMarshal.GetReference(data);
		var sum = TNumber.Zero;
		
		if (Vector.IsHardwareAccelerated && Vector<TNumber>.IsSupported)
		{
			ref var last = ref Unsafe.Add(ref first, data.Length - Vector<TNumber>.Count);

			var vectorSum = Vector<TNumber>.Zero;

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

		ref var end = ref Unsafe.Add(ref first, data.Length);

		while (Unsafe.IsAddressLessThan(ref first, ref end))
		{
			sum += first;
			first = ref Unsafe.Add(ref first, 1);
		}

		return sum;
	}
}