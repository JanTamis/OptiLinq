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
|                              Method |          Mean |       Error |      StdDev |             Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------ |--------------:|------------:|------------:|------------------:|--------:|----------:|------------:|
| OptiRangeWhereSelectSumWithDelegate |      4.692 ns |   0.0722 ns |   0.0640 ns | 1,493.327x faster |  29.99x |         - |          NA |
|             OptiRangeWhereSelectSum |     11.471 ns |   0.0768 ns |   0.0641 ns |   611.115x faster |   5.81x |         - |          NA |
|                       HandmadedCode |  7,001.519 ns |  85.5860 ns |  80.0572 ns |          baseline |         |         - |          NA |
|                             SysLinq | 56,013.095 ns | 664.6106 ns | 589.1599 ns |     7.996x slower |   0.09x |     104 B |          NA |
