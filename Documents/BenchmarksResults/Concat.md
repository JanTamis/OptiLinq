## Concat

### Source
[Concat.cs](../../src/OptiLinq.Benchmark/Concat.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------ |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
| OptiLinqSum |  5.255 μs | 0.0465 μs | 0.0435 μs | 16.57x faster |   0.21x |      32 B |  3.75x less |
|    OptiLinq | 43.135 μs | 0.3620 μs | 0.3386 μs |  2.02x faster |   0.02x |      32 B |  3.75x less |
|        Linq | 87.054 μs | 0.8222 μs | 0.6866 μs |      baseline |         |     120 B |             |
|     LinqSum | 87.241 μs | 1.1134 μs | 0.9870 μs |  1.00x slower |   0.02x |     120 B |  1.00x more |
