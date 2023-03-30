namespace OptiLinq.Comparers;

public struct ArrayComparer<T> : IComparer<T[]>
{
	public int Compare(T[]? x, T[]? y)
	{
		if (x is null && y is null)
		{
			return 0;
		}

		if (x is null)
		{
			return -1;
		}

		if (y is null)
		{
			return 1;
		}


		for (var i = 0; i < x.Length; i++)
		{
			var result = Comparer<T>.Default.Compare(x[i], y[i]);

			if (result != 0)
			{
				return result;
			}
		}

		return 0;
	}
}