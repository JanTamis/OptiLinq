using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;
using OptiLinq.Interfaces;

namespace OptiLinq.Helpers;

public static class EnumerableHelper
{
	/// <summary>Converts an enumerable to an array using the same logic as List{T}.</summary>
	/// <param name="source">The enumerable to convert.</param>
	/// <param name="length">The number of items stored in the resulting array, 0-indexed.</param>
	/// <returns>
	/// The resulting array. The length of the array may be greater than <paramref name="length"/>,
	/// which is the actual number of elements in the array.
	/// </returns>
	internal static TElement[] ToArray<TElement, TQuery, TEnumerable>(this TQuery source, out int length)
		where TEnumerable : struct, IOptiEnumerator<TElement>
		where TQuery : struct, IOptiQuery<TElement, TEnumerable>
	{
		if (source.TryGetNonEnumeratedCount(out length))
		{
			var result = GC.AllocateUninitializedArray<TElement>(length); // new TElement[length];

			length = source.CopyTo(result);

			if (length != result.Length)
			{
				Array.Resize(ref result, length);
			}

			return result;
		}

		return ToArray(source.GetEnumerator(), ArrayPool<TElement>.Shared, 4, out length);
	}

	internal static TElement[] ToArray<TElement, TEnumerable>(TEnumerable enumerable, ArrayPool<TElement> pool, int initialLength, out int length)
		where TEnumerable : IOptiEnumerator<TElement>
	{
		using var list = new PooledList<TElement>(initialLength, pool);

		while (enumerable.MoveNext())
		{
			list.Add(enumerable.Current);
		}

		enumerable.Dispose();

		length = list.Count;
		return list.ToArray();
	}

	internal static List<TElement> ToList<TElement, TQuery, TEnumerable>(this TQuery source, out int length)
		where TEnumerable : struct, IOptiEnumerator<TElement>
		where TQuery : struct, IOptiQuery<TElement, TEnumerable>
	{
		List<TElement> result;

		if (source.TryGetNonEnumeratedCount(out length))
		{
			result = new List<TElement>(length);
			source.CopyTo(CollectionsMarshal.AsSpan(result));

			return result;
		}

		result = new List<TElement>();

		using var enumerator = source.GetEnumerator();

		while (enumerator.MoveNext())
		{
			result.Add(enumerator.Current);
		}

		length = result.Count;
		return result;
	}

	internal static string Join<TElement, TEnumerable>(this TEnumerable enumerable, string separator)
		where TEnumerable : IOptiEnumerator<TElement>
	{
		if (!enumerable.MoveNext())
		{
			enumerable.Dispose();
			return String.Empty;
		}

		var builder = new ValueStringBuilder(stackalloc char[256]);

		builder.Append(enumerable.Current.ToString());

		do
		{
			builder.Append(separator);
			builder.Append(enumerable.Current.ToString());
		} while (enumerable.MoveNext());

		enumerable.Dispose();
		return builder.ToString();
	}

	internal static string JoinFormattable<TElement, TEnumerable>(this TEnumerable enumerable, string separator, ReadOnlySpan<char> format, IFormatProvider? provider = null)
		where TEnumerable : IOptiQuery<TElement>
		where TElement : ISpanFormattable
	{
		using var enumerator = enumerable.GetEnumerator();

		if (!enumerator.MoveNext())
		{
			return String.Empty;
		}

		var builder = new ValueStringBuilder(stackalloc char[256]);

		enumerator.Current.TryFormat(builder.RawChars, out var count, format, provider);
		builder.Length += count;

		while (enumerator.MoveNext())
		{
			builder.Append(separator);

			var current = enumerator.Current;

			if (current.TryFormat(builder.RawChars.Slice(builder.Length), out count, format, provider))
			{
				builder.Length += count;
			}
			else
			{
				builder.EnsureCapacity(builder.Capacity * 2);

				if (current.TryFormat(builder.RawChars.Slice(builder.Length), out count, format, provider))
				{
					builder.Length += count;
				}
			}
		}

		return builder.ToString();
	}

	public static TSource Min<TSource, TEnumerator>(TEnumerator source)
		where TEnumerator : struct, IOptiEnumerator<TSource>
	{
		TSource value = default!;

		using (source)
		{
			if (value == null)
			{
				do
				{
					if (!source.MoveNext())
					{
						return value;
					}

					value = source.Current;
				} while (value == null);

				while (source.MoveNext())
				{
					var next = source.Current;

					if (next != null && Comparer<TSource>.Default.Compare(next, value) < 0)
					{
						value = next;
					}
				}
			}
			else
			{
				if (!source.MoveNext())
				{
					throw new InvalidOperationException("Sequence contains no elements");
				}

				value = source.Current;

				while (source.MoveNext())
				{
					var next = source.Current;

					if (Comparer<TSource>.Default.Compare(next, value) < 0)
					{
						value = next;
					}
				}
			}
		}

		return value;
	}

	public static TSource Max<TSource, TEnumerator>(TEnumerator source)
		where TEnumerator : struct, IOptiEnumerator<TSource>
	{
		TSource value = default!;

		using (source)
		{
			if (value == null)
			{
				do
				{
					if (!source.MoveNext())
					{
						return value;
					}

					value = source.Current;
				} while (value == null);

				while (source.MoveNext())
				{
					var next = source.Current;
					if (next != null && Comparer<TSource>.Default.Compare(next, value) > 0)
					{
						value = next;
					}
				}
			}
			else
			{
				if (!source.MoveNext())
				{
					throw new InvalidOperationException("Sequence contains no elements");
				}

				value = source.Current;

				while (source.MoveNext())
				{
					var next = source.Current;

					if (Comparer<TSource>.Default.Compare(next, value) > 0)
					{
						value = next;
					}
				}
			}
		}

		return value;
	}

	public static bool TryGetElementAt<TSource, TEnumerator, TIndex>(TEnumerator enumerator, TIndex index, out TSource item)
		where TIndex : IBinaryInteger<TIndex>
		where TEnumerator : struct, IOptiEnumerator<TSource>
	{
		using (enumerator)
		{
			if (TIndex.IsPositive(index))
			{
				while (enumerator.MoveNext())
				{
					if (TIndex.IsZero(index))
					{
						item = enumerator.Current;
						return true;
					}

					index--;
				}
			}

			item = default!;
			return false;
		}
	}

	public static bool TryGetFirst<TSource, TEnumerator>(TEnumerator enumerator, out TSource item)
		where TEnumerator : struct, IOptiEnumerator<TSource>
	{
		using (enumerator)
		{
			if (!enumerator.MoveNext())
			{
				item = default!;
				return false;
			}

			item = enumerator.Current;
			return true;
		}
	}

	public static bool TryGetLast<TSource, TEnumerator>(TEnumerator enumerator, out TSource item)
		where TEnumerator : struct, IOptiEnumerator<TSource>
	{
		using (enumerator)
		{
			if (!enumerator.MoveNext())
			{
				item = default!;
				return false;
			}

			item = enumerator.Current;

			while (enumerator.MoveNext())
			{
				item = enumerator.Current;
			}

			return true;
		}
	}
}