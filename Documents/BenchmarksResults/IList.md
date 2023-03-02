## IList

### Source
[IList.cs](../../src/StructLinq.Benchmark/IList.cs)

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
| OptiLinq | 75.96 μs | 0.115 μs | 0.102 μs | 1.08x faster |   0.05x |      40 B |  1.00x more |
|     Linq | 87.30 μs | 2.259 μs | 6.589 μs |     baseline |         |      40 B |             |
