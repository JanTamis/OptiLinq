## ListWhereSelectSum

### Source
[ListWhereSelectSum.cs](../../src/StructLinq.Benchmark/ListWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|    OptiLinqIFunction | 23.13 μs | 0.360 μs | 0.401 μs | 2.96x faster |   0.06x |      32 B |  4.75x less |
| OptiLinqWithDelegate | 46.35 μs | 1.558 μs | 4.569 μs | 1.40x faster |   0.11x |      40 B |  3.80x less |
|                 LINQ | 68.62 μs | 0.664 μs | 0.518 μs |     baseline |         |     152 B |             |
