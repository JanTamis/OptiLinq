## IList

### Source
[IList.cs](../../src/OptiLinq.Benchmark/IList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 37.38 μs | 0.303 μs | 0.253 μs | 2.04x faster |   0.05x |         - |          NA |
|     Linq | 76.05 μs | 1.500 μs | 1.668 μs |     baseline |         |      40 B |             |
