## SumOnSelectMany

### Source
[SumOnSelectMany.cs](../../src/OptiLinq.Benchmark/SumOnSelectMany.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                          Method |       Mean |    Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated |     Alloc Ratio |
|-------------------------------- |-----------:|---------:|---------:|--------------:|--------:|-------:|----------:|----------------:|
|                        OptiLINQ |   229.3 μs |  2.09 μs |  1.85 μs | 17.56x faster |   0.22x | 2.6855 |   24032 B |     1.335x less |
|            OptiLinqWithFunction |   257.3 μs |  3.19 μs |  2.66 μs | 15.66x faster |   0.31x |      - |      32 B | 1,002.219x less |
|  OptiLINQWhereReturnIsOptiQuery |   835.4 μs |  8.66 μs |  7.68 μs |  4.82x faster |   0.07x |      - |      33 B |   971.848x less |
| OptiLINQWithFunctionWithForeach | 3,891.3 μs | 25.60 μs | 22.69 μs |  1.03x faster |   0.02x |      - |   32004 B |     1.002x less |
|                            LINQ | 4,025.4 μs | 60.67 μs | 53.78 μs |      baseline |         |      - |   32071 B |                 |
