## ImmutableArrayWhereSelectSum

### Source
[ImmutableArrayWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ImmutableArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|             LINQ | 47.32 μs | 0.468 μs | 0.415 μs |     baseline |         |     104 B |             |
| OptiLinqFunction | 59.01 μs | 0.508 μs | 0.424 μs | 1.25x slower |   0.01x |      88 B |  1.18x less |
|         OptiLinq | 81.91 μs | 0.598 μs | 0.499 μs | 1.73x slower |   0.02x |      96 B |  1.08x less |
