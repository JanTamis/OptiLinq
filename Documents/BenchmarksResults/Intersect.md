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
|           Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |   Gen1 | Allocated |     Alloc Ratio |
|----------------- |----------:|---------:|---------:|-------------:|--------:|--------:|-------:|----------:|----------------:|
| OptiLinqComparer |  57.05 μs | 1.059 μs | 0.991 μs | 1.87x faster |   0.04x |       - |      - |         - |              NA |
|      OptiLinqSum |  89.42 μs | 1.442 μs | 1.204 μs | 1.19x faster |   0.03x |       - |      - |      48 B | 1,952.167x less |
|         OptiLinq |  90.92 μs | 1.598 μs | 1.495 μs | 1.18x faster |   0.03x |       - |      - |         - |              NA |
|          LinqSum | 106.59 μs | 0.826 μs | 0.645 μs | 1.00x faster |   0.02x | 11.1084 | 2.6855 |   93704 B |     1.000x more |
|             Linq | 106.80 μs | 1.833 μs | 1.714 μs |     baseline |         | 11.1084 | 2.6855 |   93704 B |                 |
|     LinqComparer | 126.40 μs | 1.544 μs | 1.290 μs | 1.18x slower |   0.02x | 10.9863 | 2.6855 |   93728 B |     1.000x more |
