## Except

### Source
[Except.cs](../../src/StructLinq.Benchmark/Except.cs)

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
| OptiLinqSum | 159.3 μs | 2.70 μs | 2.25 μs | 1.55x faster |   0.07x |       - |       - |       - |     208 B | 1,384.995x less |
|     LinqSum | 235.6 μs | 0.65 μs | 0.55 μs | 1.05x faster |   0.04x | 45.4102 | 45.4102 | 45.4102 |  288079 B |     1.000x more |
|        Linq | 250.5 μs | 4.98 μs | 7.76 μs |     baseline |         | 45.4102 | 45.4102 | 45.4102 |  288079 B |                 |
|    OptiLinq | 254.1 μs | 4.95 μs | 6.26 μs | 1.02x slower |   0.04x | 62.0117 | 62.0117 | 62.0117 |  262468 B |     1.098x less |
