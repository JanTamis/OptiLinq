## ListWhereSelectSum

### Source
[ListWhereSelectSum.cs](../../src/OptiLinq.Benchmark/ListWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|    OptiLinqIFunction |  7.259 μs | 0.0542 μs | 0.0480 μs | 8.71x faster |   0.07x |      32 B |  4.75x less |
| OptiLinqWithDelegate | 32.566 μs | 0.2281 μs | 0.2022 μs | 1.94x faster |   0.03x |      40 B |  3.80x less |
|                 LINQ | 63.200 μs | 0.7304 μs | 0.6099 μs |     baseline |         |     152 B |             |
