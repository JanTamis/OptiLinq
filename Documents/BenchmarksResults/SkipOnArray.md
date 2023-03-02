## SkipOnArray

### Source
[SkipOnArray.cs](../../src/StructLinq.Benchmark/SkipOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|----------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 14.62 μs | 0.032 μs |  0.025 μs | 7.19x faster |   0.63x |         - |          NA |
|     Linq | 97.26 μs | 4.250 μs | 12.464 μs |     baseline |         |      48 B |             |
