## AnyOnArray

### Source
[AnyOnArray.cs](../../src/OptiLinq.Benchmark/AnyOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |        Mean |     Error |    StdDev |           Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |------------:|----------:|----------:|----------------:|--------:|-------:|----------:|------------:|
|  DelegateOptiLinq |    22.79 ns |  0.211 ns |  0.187 ns | 129.907x faster |   1.86x | 0.0067 |      56 B |  1.75x more |
| IFunctionOptiLinq |   166.09 ns |  1.782 ns |  1.580 ns |  17.826x faster |   0.22x |      - |         - |          NA |
|               For |   259.95 ns |  1.901 ns |  1.587 ns |  11.399x faster |   0.15x |      - |         - |          NA |
|              Linq | 2,960.43 ns | 32.977 ns | 29.234 ns |        baseline |         | 0.0038 |      32 B |             |
