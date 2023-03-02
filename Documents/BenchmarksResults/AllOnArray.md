## AllOnArray

### Source
[AllOnArray.cs](../../src/OptiLinq.Benchmark/AllOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |     Median |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|-----------:|--------------:|--------:|-------:|----------:|------------:|
|               For |   174.2 ns |  0.98 ns |  0.87 ns |   174.1 ns | 16.63x faster |   0.13x |      - |         - |          NA |
| IFunctionOptiLinq |   266.6 ns |  5.39 ns | 13.31 ns |   262.4 ns | 11.21x faster |   0.77x |      - |         - |          NA |
|  DelegateOptiLinq | 1,062.4 ns | 24.91 ns | 72.66 ns | 1,055.4 ns |  2.67x faster |   0.20x | 0.0019 |      24 B |  1.33x less |
|              Linq | 2,897.9 ns | 13.79 ns | 12.90 ns | 2,901.8 ns |      baseline |         | 0.0038 |      32 B |             |
