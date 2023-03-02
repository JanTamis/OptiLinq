## Repeat

### Source
[Repeat.cs](../../src/OptiLinq.Benchmark/Repeat.cs)

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
|  OptiLinqeRepeat |  2.393 μs | 0.0447 μs | 0.0418 μs | 13.66x faster |   0.25x |         - |          NA |
| EnumerableRepeat | 32.745 μs | 0.2343 μs | 0.1957 μs |      baseline |         |      32 B |             |
