## Intersect

### Source
[Intersect.cs](../../src/OptiLinq.Benchmark/Intersect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |   Alloc Ratio |
|----------------- |----------:|---------:|---------:|-------------:|--------:|--------:|--------:|--------:|----------:|--------------:|
|      OptiLinqSum |  82.99 μs | 0.927 μs | 0.868 μs | 1.17x faster |   0.02x |       - |       - |       - |     112 B | 836.643x less |
|          LinqSum |  97.07 μs | 0.742 μs | 0.658 μs | 1.00x faster |   0.01x | 11.1084 |  2.6855 |       - |   93704 B |   1.000x more |
|             Linq |  97.39 μs | 0.921 μs | 0.719 μs |     baseline |         | 11.1084 |  2.6855 |       - |   93704 B |               |
|     LinqComparer | 116.96 μs | 0.632 μs | 0.591 μs | 1.20x slower |   0.01x | 11.1084 |  2.6855 |       - |   93728 B |   1.000x more |
| OptiLinqComparer | 174.54 μs | 1.425 μs | 1.113 μs | 1.79x slower |   0.02x | 62.2559 | 62.2559 | 62.2559 |  269943 B |   2.881x more |
|         OptiLinq | 212.63 μs | 1.757 μs | 1.557 μs | 2.19x slower |   0.02x | 62.2559 | 62.2559 | 62.2559 |  270163 B |   2.883x more |
