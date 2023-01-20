// using OptiLinq.Interfaces;
//
// namespace OptiLinq;
//
// public readonly struct EnumerableQuery<T> : IOptiQuery<T, IOptiEnumerator<T>>
// {
// 	private readonly IEnumerable<T> _enumerable;
//
// 	internal EnumerableQuery(IEnumerable<T> enumerable)
// 	{
// 		_enumerable = enumerable;
// 	}
//
// 	public bool Contains(T item, IEqualityComparer<T>? comparer = null)
// 	{
// 		comparer ??= EqualityComparer<T>.Default;
//
// 		using var enumerator = GetEnumerator();
//
// 		while (enumerator.MoveNext())
// 		{
// 			if (comparer.Equals(item, enumerator.Current))
// 			{
// 				return true;
// 			}
// 		}
//
// 		return false;
// 	}
//
// 	public int Count()
// 	{
// 		return _enumerable.Count();
// 	}
//
// 	public IOptiEnumerator<T> GetEnumerator()
// 	{
// 		return _enumerable.GetEnumerator();
// 	}
//
// 	public T[] ToArray(out int length)
// 	{
// 		if (_enumerable is ICollection<T> collection)
// 		{
// 			length = collection.Count;
//
// 			if (length == 0)
// 			{
// 				return Array.Empty<T>();
// 			}
//
// 			var result = new T[length];
// 			collection.CopyTo(result, arrayIndex: 0);
//
// 			return result;
// 		}
//
// 		var enumerator = GetEnumerator();
//
// 		if (enumerator.MoveNext())
// 		{
// 			var arr = GC.AllocateUninitializedArray<T>(4);
// 			var count = 1;
//
// 			arr[0] = enumerator.Current;
//
// 			while (enumerator.MoveNext())
// 			{
// 				if (count == arr.Length)
// 				{
// 					// This is the same growth logic as in List<T>:
// 					// If the array is currently empty, we make it a default size.  Otherwise, we attempt to
// 					// double the size of the array.  Doubling will overflow once the size of the array reaches
// 					// 2^30, since doubling to 2^31 is 1 larger than Int32.MaxValue.  In that case, we instead
// 					// constrain the length to be Array.MaxLength (this overflow check works because of the
// 					// cast to uint).
// 					var newLength = count << 1;
// 					if ((uint)newLength > Array.MaxLength)
// 					{
// 						newLength = Array.MaxLength <= count ? count + 1 : Array.MaxLength;
// 					}
//
// 					Array.Resize(ref arr, newLength);
//
// 					var lArr = arr;
// 					arr = GC.AllocateUninitializedArray<T>(newLength);
//
// 					lArr.CopyTo(arr, 0);
// 				}
//
// 				arr[count++] = enumerator.Current;
// 			}
//
// 			length = count;
// 			return arr;
// 		}
//
// 		length = 0;
// 		return Array.Empty<T>();
// 	}
//
// 	public List<T> ToList()
// 	{
// 		return _enumerable.ToList();
// 	}
//
// 	public bool TryGetNonEnumeratedCount(out int length)
// 	{
// 		return _enumerable.TryGetNonEnumeratedCount(out length);
// 	}
// }