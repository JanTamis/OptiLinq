## ArraySelectCount

### Source
[ArraySelectCount.cs](../../src/OptiLinq.Benchmark/ArraySelectCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|          Method |           Mean |       Error |      StdDev |         Median |              Ratio |   RatioSD | Allocated | Alloc Ratio |
|---------------- |---------------:|------------:|------------:|---------------:|-------------------:|----------:|----------:|------------:|
| IFunctionSelect |      0.0227 ns |   0.0299 ns |   0.0249 ns |      0.0156 ns |                 NA |        NA |         - |          NA |
|  DelegateSelect |      0.6393 ns |   0.0271 ns |   0.0226 ns |      0.6433 ns | 28,194.076x faster | 1,025.78x |         - |          NA |
|            Linq | 18,021.1511 ns | 289.9911 ns | 257.0696 ns | 17,998.7965 ns |           baseline |           |      48 B |             |
