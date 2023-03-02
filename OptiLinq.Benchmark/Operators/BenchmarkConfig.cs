using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace OptiLinq.Benchmark;

public class BenchmarkConfig : ManualConfig
{
	public BenchmarkConfig()
	{
		SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
	}
}