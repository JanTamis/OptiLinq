using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace OptiLinq.Collections;

public class SpanHelper
{
	internal static bool ContainsValueType<T>(ref T searchSpace, T value, int length) where T : struct, INumber<T>
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
}