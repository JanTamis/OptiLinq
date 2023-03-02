## Intersect

### Source
[Intersect.cs](../../src/StructLinq.Benchmark/Intersect.cs)

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
|      OptiLinqSum |  91.26 μs | 1.039 μs | 0.921 μs | 1.20x faster |   0.01x |       - |       - |       - |     112 B | 836.643x less |
|             Linq | 109.11 μs | 0.252 μs | 0.211 μs |     baseline |         | 11.1084 |  2.6855 |       - |   93704 B |               |
|          LinqSum | 110.97 μs | 1.479 μs | 1.383 μs | 1.02x slower |   0.01x | 11.1084 |  2.6855 |       - |   93704 B |   1.000x more |
|     LinqComparer | 131.28 μs | 1.649 μs | 1.542 μs | 1.20x slower |   0.02x | 10.9863 |  2.6855 |       - |   93728 B |   1.000x more |
| OptiLinqComparer | 190.18 μs | 2.769 μs | 2.590 μs | 1.74x slower |   0.02x | 62.2559 | 62.2559 | 62.2559 |  266546 B |   2.845x more |
|         OptiLinq | 230.60 μs | 2.842 μs | 3.159 μs | 2.12x slower |   0.03x | 62.2559 | 62.2559 | 62.2559 |  266496 B |   2.844x more |
