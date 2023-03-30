## SumOnSelectMany

### Source
[SumOnSelectMany.cs](../../src/OptiLinq.Benchmark/SumOnSelectMany.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                          Method |            Mean |         Error |        StdDev |               Ratio |   RatioSD |   Gen0 | Allocated |     Alloc Ratio |
|-------------------------------- |----------------:|--------------:|--------------:|--------------------:|----------:|-------:|----------:|----------------:|
| OptiLINQWithFunctionWithForeach |        17.91 ns |      0.206 ns |      0.193 ns | 245,768.116x faster | 4,503.01x |      - |         - |              NA |
|            OptiLinqWithFunction |        29.89 ns |      0.387 ns |      0.343 ns | 147,392.579x faster | 2,926.58x | 0.0038 |      32 B | 1,002.219x less |
|                        OptiLINQ |        32.57 ns |      0.447 ns |      0.397 ns | 135,257.650x faster | 2,038.96x | 0.0038 |      32 B | 1,002.219x less |
|  OptiLINQWhereReturnIsOptiQuery |        32.75 ns |      0.676 ns |      0.947 ns | 132,905.300x faster | 5,352.56x | 0.0038 |      32 B | 1,002.219x less |
|                            LINQ | 4,404,863.99 ns | 63,158.668 ns | 55,988.514 ns |            baseline |           |      - |   32071 B |                 |
