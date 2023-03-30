using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace OptiLinq.Collections;

internal class SpanHelper
{
	public static bool ContainsValueType<T>(ref T searchSpace, T value, int length) where T : struct, INumber<T>
	{
		if (!Vector128.IsHardwareAccelerated || length < Vector128<T>.Count)
		{
			nuint offset = 0;

			while (length >= 8)
			{
				length -= 8;

				if (Unsafe.Add(ref searchSpace, offset) == value
				    || Unsafe.Add(ref searchSpace, offset + 1) == value
				    || Unsafe.Add(ref searchSpace, offset + 2) == value
				    || Unsafe.Add(ref searchSpace, offset + 3) == value
				    || Unsafe.Add(ref searchSpace, offset + 4) == value
				    || Unsafe.Add(ref searchSpace, offset + 5) == value
				    || Unsafe.Add(ref searchSpace, offset + 6) == value
				    || Unsafe.Add(ref searchSpace, offset + 7) == value)
				{
					return true;
				}

				offset += 8;
			}

			if (length >= 4)
			{
				length -= 4;

				if (Unsafe.Add(ref searchSpace, offset) == value
				    || Unsafe.Add(ref searchSpace, offset + 1) == value
				    || Unsafe.Add(ref searchSpace, offset + 2) == value
				    || Unsafe.Add(ref searchSpace, offset + 3) == value)
				{
					return true;
				}

				offset += 4;
			}

			while (length > 0)
			{
				length -= 1;

				if (Unsafe.Add(ref searchSpace, offset) == value) return true;

				offset += 1;
			}
		}
		else if (Vector256.IsHardwareAccelerated && length >= Vector256<T>.Count)
		{
			Vector256<T> equals, values = Vector256.Create(value);
			ref var currentSearchSpace = ref searchSpace;
			ref var oneVectorAwayFromEnd = ref Unsafe.Add(ref searchSpace, length - Vector256<T>.Count);

			// Loop until either we've finished all elements or there's less than a vector's-worth remaining.
			do
			{
				equals = Vector256.Equals(values, Vector256.LoadUnsafe(ref currentSearchSpace));
				if (equals == Vector256<T>.Zero)
				{
					currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector256<T>.Count);
					continue;
				}

				return true;
			} while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

			// If any elements remain, process the last vector in the search space.
			if ((uint)length % Vector256<T>.Count != 0)
			{
				equals = Vector256.Equals(values, Vector256.LoadUnsafe(ref oneVectorAwayFromEnd));
				if (equals != Vector256<T>.Zero)
				{
					return true;
				}
			}
		}
		else
		{
			Vector128<T> equals, values = Vector128.Create(value);
			ref var currentSearchSpace = ref searchSpace;
			ref var oneVectorAwayFromEnd = ref Unsafe.Add(ref searchSpace, length - Vector128<T>.Count);

			// Loop until either we've finished all elements or there's less than a vector's-worth remaining.
			do
			{
				equals = Vector128.Equals(values, Vector128.LoadUnsafe(ref currentSearchSpace));
				if (equals == Vector128<T>.Zero)
				{
					currentSearchSpace = ref Unsafe.Add(ref currentSearchSpace, Vector128<T>.Count);
					continue;
				}

				return true;
			} while (!Unsafe.IsAddressGreaterThan(ref currentSearchSpace, ref oneVectorAwayFromEnd));

			// If any elements remain, process the first vector in the search space.
			if ((uint)length % Vector128<T>.Count != 0)
			{
				equals = Vector128.Equals(values, Vector128.LoadUnsafe(ref oneVectorAwayFromEnd));
				if (equals != Vector128<T>.Zero)
				{
					return true;
				}
			}
		}

		return false;
	}

	/// <summary>
	/// Casts a Span of one primitive type <typeparamref name="TFrom"/> to another primitive type <typeparamref name="TTo"/>.
	/// These types may not contain pointers or references. This is checked at runtime in order to preserve type safety.
	/// </summary>
	/// <remarks>
	/// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other means.
	/// </remarks>
	/// <param name="span">The source slice, of type <typeparamref name="TFrom"/>.</param>
	/// <exception cref="System.ArgumentException">
	/// Thrown when <typeparamref name="TFrom"/> or <typeparamref name="TTo"/> contains pointers.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span)
	{
		// Use unsigned integers - unsigned division by constant (especially by power of 2)
		// and checked casts are faster and smaller.
		uint fromSize = (uint)Unsafe.SizeOf<TFrom>();
		uint toSize = (uint)Unsafe.SizeOf<TTo>();
		uint fromLength = (uint)span.Length;
		int toLength;
		if (fromSize == toSize)
		{
			// Special case for same size types - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
			// should be optimized to just `length` but the JIT doesn't do that today.
			toLength = (int)fromLength;
		}
		else if (fromSize == 1)
		{
			// Special case for byte sized TFrom - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
			// becomes `(ulong)fromLength / (ulong)toSize` but the JIT can't narrow it down to `int`
			// and can't eliminate the checked cast. This also avoids a 32 bit specific issue,
			// the JIT can't eliminate long multiply by 1.
			toLength = (int)(fromLength / toSize);
		}
		else
		{
			// Ensure that casts are done in such a way that the JIT is able to "see"
			// the uint->ulong casts and the multiply together so that on 32 bit targets
			// 32x32to64 multiplication is used.
			ulong toLengthUInt64 = (ulong)fromLength * (ulong)fromSize / (ulong)toSize;
			toLength = checked((int)toLengthUInt64);
		}

		return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<TFrom, TTo>(ref MemoryMarshal.GetReference(span)), toLength);
	}
}