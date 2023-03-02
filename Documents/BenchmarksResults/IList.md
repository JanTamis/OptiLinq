## IList

### Source
[IList.cs](../../src/OptiLinq.Benchmark/IList.cs)

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
| OptiLinq | 72.04 μs | 1.428 μs | 2.048 μs | 1.02x faster |   0.03x |      40 B |  1.00x more |
|     Linq | 73.80 μs | 1.042 μs | 0.975 μs |     baseline |         |      40 B |             |
