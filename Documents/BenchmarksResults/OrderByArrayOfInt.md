## OrderByArrayOfInt

### Source
[OrderByArrayOfInt.cs](../../src/OptiLinq.Benchmark/OrderByArrayOfInt.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |       Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 | Allocated |     Alloc Ratio |
|--------------------- |-----------:|---------:|---------:|-------------:|--------:|--------:|----------:|----------------:|
|              LINQSum |         NA |       NA |       NA |            ? |       ? |       - |         - |               ? |
|     OptiLinqOrderSum |   748.7 μs | 13.71 μs | 12.83 μs | 2.88x faster |   0.07x |       - |      33 B | 3,953.091x less |
|        OptiLinqOrder |   794.6 μs | 13.31 μs | 11.80 μs | 2.71x faster |   0.05x |       - |      65 B | 2,006.954x less |
| OptiLinqOrderFuncSum | 1,809.8 μs | 34.78 μs | 38.66 μs | 1.19x faster |   0.02x |       - |     171 B |   762.877x less |
|    OptiLinqOrderFunc | 1,837.9 μs | 29.79 μs | 27.87 μs | 1.17x faster |   0.03x |       - |     131 B |   995.817x less |
|                 LINQ | 2,154.8 μs | 40.99 μs | 38.34 μs |     baseline |         | 11.7188 |  130452 B |                 |

Benchmarks with issues:
  OrderByArrayOfInt.LINQSum: DefaultJob
