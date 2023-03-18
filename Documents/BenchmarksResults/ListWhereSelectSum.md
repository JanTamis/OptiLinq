## ListWhereSelectSum

### Source
[ListWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ListWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|    OptiLinqIFunction | 17.48 μs | 0.157 μs | 0.131 μs | 3.77x faster |   0.08x |      32 B |  4.75x less |
| OptiLinqWithDelegate | 42.78 μs | 0.763 μs | 0.937 μs | 1.54x faster |   0.05x |      40 B |  3.80x less |
|                 LINQ | 65.76 μs | 1.275 μs | 1.252 μs |     baseline |         |     152 B |             |
