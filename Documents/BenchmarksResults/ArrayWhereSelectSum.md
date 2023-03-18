## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                              Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------ |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|                       HandmadedCode |  7.033 μs | 0.1245 μs | 0.1104 μs |     baseline |         |         - |          NA |
|             OptiRangeWhereSelectSum | 27.354 μs | 0.2152 μs | 0.1797 μs | 3.90x slower |   0.06x |      32 B |          NA |
|                             SysLinq | 47.427 μs | 0.4039 μs | 0.3372 μs | 6.76x slower |   0.11x |     104 B |          NA |
| OptiRangeWhereSelectSumWithDelegate | 49.350 μs | 0.7894 μs | 0.6592 μs | 7.04x slower |   0.14x |      40 B |          NA |
