using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using OptiLinq;
using StructLinq;

// var array = new int[50];
//
// for (var i = 0; i < array.Length; i++)
// {
// 	array[i] = Random.Shared.Next();
// }
//
// var temp = array
// 	.AsOptiQuery()
// 	.Where(w => w % 2 == 0)
// 	.Select(s => Double.Sqrt(s))
// 	.Order()
// 	.ToArray();

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser(false)]
// [ShortRunJob]
[Config(typeof(MyConfig))]
public class Benchmark
{
	public const int Length = 10_000;

	private int[] array;

	[GlobalSetup]
	public void Setup()
	{
		array = new int[Length];
		var rng = new System.Random(420);

		for (var i = 0; i < array.Length; i++)
		{
			array[i] = rng.Next();
		}
	}

	[Benchmark]
	public double[] LINQ()
	{
		return array
			.Where(w => w % 2 == 0)
			.Select(s => Double.Sqrt(s))
			.Order()
			.ToArray();
	}

	[Benchmark(Baseline = true)]
	public double[] OPTILINQ()
	{
		return array
			.AsOptiQuery()
			.Where<IsOdd>()
			.Select<SquareRoot, double>()
			.Order()
			.ToArray();
	}

	[Benchmark]
	public double[] STRUCTLINQ()
	{
		return StructEnumerable
			.ToStructEnumerable(array)
			.Where(w => w % 2 == 0, x => x)
			.Select(s => Double.Sqrt(s), x => x)
			.Order()
			.ToArray();
	}

	private class MyConfig : ManualConfig
	{
		public MyConfig()
		{
			SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
		}
	}
}

struct IsOdd : OptiLinq.Interfaces.IFunction<int, bool>
{
	public static bool Eval(int item)
	{
		return item % 2 == 0;
	}
}

struct SquareRoot : OptiLinq.Interfaces.IFunction<int, double>
{
	public static double Eval(int item)
	{
		return Double.Sqrt(item);
	}
}

struct Random : OptiLinq.Interfaces.IFunction<int, int>
{
	private static readonly System.Random rng = new System.Random(420);

	public static int Eval(int item)
	{
		return rng.Next();
	}
}