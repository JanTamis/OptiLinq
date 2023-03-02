## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                              Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------ |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|                       HandmadedCode |  6.845 μs | 0.0355 μs | 0.0315 μs |     baseline |         |         - |          NA |
|             OptiRangeWhereSelectSum | 11.423 μs | 0.1080 μs | 0.0957 μs | 1.67x slower |   0.02x |      32 B |          NA |
| OptiRangeWhereSelectSumWithDelegate | 41.616 μs | 0.7863 μs | 0.8075 μs | 6.10x slower |   0.13x |      40 B |          NA |
|                             SysLinq | 46.734 μs | 0.6122 μs | 0.5112 μs | 6.83x slower |   0.09x |     104 B |          NA |
