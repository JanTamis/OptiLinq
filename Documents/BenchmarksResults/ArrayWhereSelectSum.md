## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../src/StructLinq.Benchmark/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                              Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------ |----------:|----------:|----------:|------:|--------:|----------:|------------:|
|                       HandmadedCode |  7.414 μs | 0.1279 μs | 0.1473 μs |  1.00 |    0.00 |         - |          NA |
|                             SysLinq | 50.075 μs | 0.5984 μs | 0.5597 μs |  6.73 |    0.11 |     104 B |          NA |
| OptiRangeWhereSelectSumWithDelegate | 38.391 μs | 0.4932 μs | 0.4372 μs |  5.15 |    0.14 |      40 B |          NA |
|             OptiRangeWhereSelectSum | 12.164 μs | 0.1329 μs | 0.1110 μs |  1.63 |    0.04 |      32 B |          NA |
