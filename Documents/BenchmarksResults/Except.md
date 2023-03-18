## Except

### Source
[Except.cs](../../src/OptiLinq.Benchmark/Except.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |     Alloc Ratio |
|------------ |---------:|--------:|--------:|-------------:|--------:|--------:|--------:|--------:|----------:|----------------:|
| OptiLinqSum | 111.8 μs | 1.24 μs | 1.10 μs | 1.93x faster |   0.12x |       - |       - |       - |     144 B | 2,000.549x less |
|    OptiLinq | 122.9 μs | 0.59 μs | 0.46 μs | 1.77x faster |   0.12x |       - |       - |       - |      48 B | 6,001.646x less |
|     LinqSum | 211.7 μs | 3.86 μs | 3.61 μs | 1.02x faster |   0.07x | 45.4102 | 45.4102 | 45.4102 |  288079 B |     1.000x more |
|        Linq | 215.0 μs | 3.75 μs | 8.83 μs |     baseline |         | 45.4102 | 45.4102 | 45.4102 |  288079 B |                 |
