## AnyOnArray

### Source
[AnyOnArray.cs](../../src/OptiLinq.Benchmark/AnyOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|--------------:|--------:|-------:|----------:|------------:|
|               For |   265.1 ns |  3.69 ns |  3.45 ns | 11.22x faster |   0.19x |      - |         - |          NA |
| IFunctionOptiLinq |   267.8 ns |  5.09 ns |  5.86 ns | 11.14x faster |   0.33x |      - |         - |          NA |
|  DelegateOptiLinq | 1,048.2 ns | 16.68 ns | 15.60 ns |  2.85x faster |   0.05x | 0.0019 |      24 B |  1.33x less |
|              Linq | 2,982.3 ns | 34.98 ns | 27.31 ns |      baseline |         | 0.0038 |      32 B |             |
