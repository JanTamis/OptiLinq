## RangeWhereSelectSum

### Source
[RangeWhereSelectSum.cs](../../src/OptiLinq.Benchmark/RangeWhereSelectSum.cs)

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
|                            SysSum |  6.828 μs | 0.1361 μs | 0.2959 μs |      baseline |         |         - |          NA |
|           OptiRangeWhereSelectSum |  6.861 μs | 0.0697 μs | 0.0582 μs |  1.01x slower |   0.06x |      32 B |          NA |
| ConvertWhereSelectSumWithDelegate | 74.048 μs | 1.0924 μs | 1.0218 μs | 10.89x slower |   0.58x |      80 B |          NA |
|            SysRangeWhereSelectSum | 74.846 μs | 1.1994 μs | 1.1219 μs | 11.01x slower |   0.47x |     160 B |          NA |
