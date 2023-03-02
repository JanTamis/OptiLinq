## ImmutableArrayWhereSelectSum

### Source
[ImmutableArrayWhereSelectSum.cs](../../src/StructLinq.Benchmark/ImmutableArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |      Mean |    Error |    StdDev |   Median |        Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |----------:|---------:|----------:|---------:|-------------:|--------:|----------:|------------:|
|             LINQ |  53.36 μs | 0.708 μs |  0.662 μs | 53.03 μs |     baseline |         |     104 B |             |
| OptiLinqFunction |  78.18 μs | 3.665 μs | 10.575 μs | 75.28 μs | 1.40x slower |   0.21x |      88 B |  1.18x less |
|         OptiLinq | 103.11 μs | 3.416 μs | 10.073 μs | 99.61 μs | 1.99x slower |   0.23x |      96 B |  1.08x less |
