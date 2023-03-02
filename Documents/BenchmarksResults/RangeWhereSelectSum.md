## RangeWhereSelectSum

### Source
[RangeWhereSelectSum.cs](../../src/StructLinq.Benchmark/RangeWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                            Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|                            SysSum |  7.728 μs | 0.0061 μs | 0.0054 μs |      baseline |         |         - |          NA |
|           OptiRangeWhereSelectSum | 10.034 μs | 0.1082 μs | 0.0844 μs |  1.30x slower |   0.01x |      32 B |          NA |
| ConvertWhereSelectSumWithDelegate | 86.480 μs | 0.3156 μs | 0.2464 μs | 11.19x slower |   0.03x |      80 B |          NA |
|            SysRangeWhereSelectSum | 87.319 μs | 0.2547 μs | 0.1988 μs | 11.30x slower |   0.03x |     160 B |          NA |
