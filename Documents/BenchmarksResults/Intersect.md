## Intersect

### Source
[Intersect.cs](../../src/OptiLinq.Benchmark/Intersect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |   Gen1 | Allocated |      Alloc Ratio |
|----------------- |----------:|---------:|---------:|-------------:|--------:|--------:|-------:|----------:|-----------------:|
|         OptiLinq |  51.85 μs | 0.894 μs | 0.836 μs | 1.96x faster |   0.03x |       - |      - |       8 B | 11,713.000x less |
| OptiLinqComparer |  54.47 μs | 0.827 μs | 0.773 μs | 1.86x faster |   0.04x |       - |      - |       8 B | 11,713.000x less |
|      OptiLinqSum |  55.74 μs | 0.784 μs | 0.733 μs | 1.82x faster |   0.04x |       - |      - |      48 B |  1,952.167x less |
|             Linq | 101.40 μs | 1.485 μs | 1.389 μs |     baseline |         | 11.1084 | 2.6855 |   93704 B |                  |
|          LinqSum | 102.55 μs | 1.925 μs | 1.890 μs | 1.01x slower |   0.03x | 11.1084 | 2.6855 |   93704 B |      1.000x more |
|     LinqComparer | 121.85 μs | 2.026 μs | 1.895 μs | 1.20x slower |   0.02x | 11.1084 | 2.6855 |   93728 B |      1.000x more |
