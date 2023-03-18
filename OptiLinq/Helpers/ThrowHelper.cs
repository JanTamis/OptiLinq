namespace OptiLinq.Collections;

public static class ThrowHelper
{
	public static Exception CreateInfiniteException()
	{
		return new Exception("Sequence is infinite, use the Take method to make it non infinite");
	}

	public static IndexOutOfRangeException CreateOutOfRangeException()
	{
		return new IndexOutOfRangeException("Index was out of bounds");
	}
}