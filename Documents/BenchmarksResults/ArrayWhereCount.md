## ArrayWhereCount

### Source
[ArrayWhereCount.cs](../../src/StructLinq.Benchmark/ArrayWhereCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
|  HandmadedCode |  7.372 μs | 0.0834 μs | 0.0780 μs |  1.00 |    0.00 |         - |          NA |
|        SysLinq | 25.544 μs | 0.4137 μs | 0.3870 μs |  3.47 |    0.07 |      48 B |          NA |
|  DelegateWhere | 25.091 μs | 0.4975 μs | 0.6642 μs |  3.40 |    0.11 |         - |          NA |
| IFunctionWhere |  7.445 μs | 0.1469 μs | 0.1692 μs |  1.01 |    0.02 |         - |          NA |
