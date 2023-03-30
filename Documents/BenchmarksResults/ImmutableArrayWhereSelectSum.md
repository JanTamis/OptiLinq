## ImmutableArrayWhereSelectSum

### Source
[ImmutableArrayWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ImmutableArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|             LINQ | 49.36 μs | 0.431 μs | 0.404 μs |     baseline |         |     104 B |             |
| OptiLinqFunction | 72.18 μs | 0.313 μs | 0.261 μs | 1.46x slower |   0.01x |      56 B |  1.86x less |
|         OptiLinq | 95.89 μs | 1.014 μs | 0.899 μs | 1.94x slower |   0.03x |      64 B |  1.62x less |
