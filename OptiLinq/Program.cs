using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using OptiLinq;
using OptiLinq.Interfaces;

// var query = OptiQuery
// 	.Range(0, 50)
// 	.Where<IsOdd>()
// 	.Select<MultiplyByFive>()
// 	.Contains(5);

BenchmarkRunner.Run<Benchmark>();
//Console.ReadLine();

[MemoryDiagnoser(false)]
[Config(typeof(MyConfig))]
public class Benchmark
{
	private const int length = 10;

	[Benchmark]
	public int LINQ()
	{
		return Enumerable
			.Range(0, length)
			.Where(w => w % 2 == 0)
			.Select(s => s * 5)
			.Count();
	}

	[Benchmark(Baseline = true)]
	public int OPTILINQ()
	{
		return OptiQuery
			.Range(0, length)
			.Where<IsOdd>()
			.Select<MultiplyByFive>()
			.Count();
	}

	[Benchmark]
	public int TRADITIONAL()
	{
		var count = 0;
		
		for (var i = 0; i < length; i++)
		{
			if (i % 2 == 0)
			{
				count++;
			}
		}

		return count;
	}

	private class MyConfig : ManualConfig
	{
		public MyConfig()
		{
			SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
		}
	}
}

struct IsOdd : IWhereOperator<int>
{
	public static bool IsAccepted(int item)
	{
		return item % 2 == 0;
	}
}

struct MultiplyByFive : ISelectOperator<int, int>
{
	public static int Transform(int item)
	{
		return item * 5;
	}
}