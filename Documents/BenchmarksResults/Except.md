## Except

### Source
[Except.cs](../../src/OptiLinq.Benchmark/Except.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |     Alloc Ratio |
|------------ |---------:|--------:|--------:|-------------:|--------:|--------:|--------:|--------:|----------:|----------------:|
| OptiLinqSum | 126.1 μs | 1.44 μs | 1.28 μs | 1.70x faster |   0.04x |       - |       - |       - |     208 B | 1,384.995x less |
|     LinqSum | 212.6 μs | 2.23 μs | 1.98 μs | 1.01x faster |   0.01x | 45.4102 | 45.4102 | 45.4102 |  288079 B |     1.000x more |
|        Linq | 213.8 μs | 4.00 μs | 3.75 μs |     baseline |         | 45.4102 | 45.4102 | 45.4102 |  288079 B |                 |
|    OptiLinq | 216.0 μs | 2.92 μs | 2.59 μs | 1.01x slower |   0.02x | 62.2559 | 62.2559 | 62.2559 |  269903 B |     1.067x less |
