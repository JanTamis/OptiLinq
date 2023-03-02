## Repeat

### Source
[Repeat.cs](../../src/StructLinq.Benchmark/Repeat.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|  OptiLinqeRepeat |  2.873 μs | 0.0365 μs | 0.0341 μs | 14.74x faster |   0.90x |         - |          NA |
| EnumerableRepeat | 44.510 μs | 1.8063 μs | 5.2403 μs |      baseline |         |      32 B |             |
