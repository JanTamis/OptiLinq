## AllOnArray

### Source
[AllOnArray.cs](../../src/OptiLinq.Benchmark/AllOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |        Mean |     Error |     StdDev |      Median |           Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |------------:|----------:|-----------:|------------:|----------------:|--------:|-------:|----------:|------------:|
|  DelegateOptiLinq |    23.32 ns |  0.293 ns |   0.274 ns |    23.30 ns | 132.571x faster |   7.28x | 0.0067 |      56 B |  1.75x more |
| IFunctionOptiLinq |   164.84 ns |  1.041 ns |   0.923 ns |   164.76 ns |  18.781x faster |   1.03x |      - |         - |          NA |
|               For |   182.98 ns |  3.696 ns |   7.877 ns |   178.75 ns |  17.617x faster |   1.26x |      - |         - |          NA |
|              Linq | 3,202.84 ns | 63.763 ns | 182.947 ns | 3,190.36 ns |        baseline |         | 0.0038 |      32 B |             |
