## SkipOnArrayWhere

### Source
[SkipOnArrayWhere.cs](../../src/OptiLinq.Benchmark/SkipOnArrayWhere.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 38.83 μs | 0.291 μs | 0.243 μs | 2.41x faster |   0.03x |         - |          NA |
|     Linq | 93.56 μs | 0.704 μs | 0.659 μs |     baseline |         |     104 B |             |
