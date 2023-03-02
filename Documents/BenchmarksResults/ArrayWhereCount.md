## ArrayWhereCount

### Source
[ArrayWhereCount.cs](../../src/OptiLinq.Benchmark/ArrayWhereCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |     Error |    StdDev |    Median |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|  HandmadedCode |  6.791 μs | 0.1332 μs | 0.2535 μs |  6.748 μs |     baseline |         |         - |          NA |
| IFunctionWhere |  6.879 μs | 0.0425 μs | 0.0355 μs |  6.891 μs | 1.02x faster |   0.04x |         - |          NA |
|  DelegateWhere | 23.443 μs | 0.2072 μs | 0.1836 μs | 23.410 μs | 3.35x slower |   0.13x |         - |          NA |
|        SysLinq | 24.969 μs | 0.5603 μs | 1.6521 μs | 24.151 μs | 3.81x slower |   0.30x |      48 B |          NA |
