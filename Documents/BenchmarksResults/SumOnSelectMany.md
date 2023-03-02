## SumOnSelectMany

### Source
[SumOnSelectMany.cs](../../src/StructLinq.Benchmark/SumOnSelectMany.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                           Method |       Mean |    Error |  StdDev |         Ratio | RatioSD |   Gen0 | Allocated |     Alloc Ratio |
|--------------------------------- |-----------:|---------:|--------:|--------------:|--------:|-------:|----------:|----------------:|
|                         OptiLINQ |   288.5 μs |  1.47 μs | 1.38 μs | 17.78x faster |   0.08x | 2.4414 |   24032 B |     1.335x less |
|             OptiLinqWithFunction |   307.6 μs |  0.42 μs | 0.35 μs | 16.67x faster |   0.04x |      - |      32 B | 1,002.219x less |
| StructLINQWhereReturnIsOptiQuery | 1,142.7 μs |  0.91 μs | 0.71 μs |  4.49x faster |   0.01x |      - |      34 B |   943.265x less |
|  OptiLINQWithFunctionWithForeach | 4,444.2 μs | 10.91 μs | 9.11 μs |  1.15x faster |   0.00x |      - |   32007 B |     1.002x less |
|                             LINQ | 5,128.5 μs | 10.08 μs | 8.42 μs |      baseline |         |      - |   32071 B |                 |
