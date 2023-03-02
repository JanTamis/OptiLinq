## TakeOnArrayWhere

### Source
[TakeOnArrayWhere.cs](../../src/OptiLinq.Benchmark/TakeOnArrayWhere.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------ |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|    OptiLinq | 20.98 μs | 0.284 μs | 0.251 μs | 2.29x faster |   0.04x |         - |          NA |
| OptiLinqSum | 21.96 μs | 0.220 μs | 0.195 μs | 2.18x faster |   0.02x |      40 B |  2.60x less |
|        Linq | 48.01 μs | 0.487 μs | 0.406 μs |     baseline |         |     104 B |             |
|     LinqSum | 49.61 μs | 0.885 μs | 0.828 μs | 1.03x slower |   0.01x |     104 B |  1.00x more |
