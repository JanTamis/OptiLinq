## Aggregate

### Source
[Aggregate.cs](../../src/OptiLinq.Benchmark/Aggregate.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
| IFunctionAggregate |  2.613 μs | 0.0358 μs | 0.0335 μs | 19.83x faster |   0.28x |         - |          NA |
|   ConvertAggregate | 42.280 μs | 0.5260 μs | 0.4663 μs |  1.22x faster |   0.01x |      40 B |  1.00x more |
|       SysAggregate | 51.853 μs | 0.2395 μs | 0.1870 μs |      baseline |         |      40 B |             |
|  DelegateAggregate | 84.044 μs | 1.6255 μs | 1.8068 μs |  1.62x slower |   0.04x |      64 B |  1.60x more |
