## ListWhereSelectSum

### Source
[ListWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ListWhereSelectSum.cs)

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
|    OptiLinqIFunction | 19.00 μs | 0.167 μs | 0.140 μs | 3.19x faster |   0.03x |      32 B |  4.75x less |
| OptiLinqWithDelegate | 33.89 μs | 0.532 μs | 0.591 μs | 1.79x faster |   0.03x |      40 B |  3.80x less |
|                 LINQ | 60.52 μs | 0.433 μs | 0.384 μs |     baseline |         |     152 B |             |
