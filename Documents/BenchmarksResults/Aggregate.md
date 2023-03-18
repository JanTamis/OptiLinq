## Aggregate

### Source
[Aggregate.cs](../../src/OptiLinq.Benchmark/Aggregate.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
| IFunctionAggregate |  2.722 μs | 0.0487 μs | 0.0455 μs | 19.97x faster |   0.49x |         - |          NA |
|  DelegateAggregate | 24.372 μs | 0.4498 μs | 0.4207 μs |  2.23x faster |   0.06x |      32 B |  1.25x less |
|   ConvertAggregate | 43.619 μs | 0.4660 μs | 0.4359 μs |  1.25x faster |   0.03x |      40 B |  1.00x more |
|       SysAggregate | 54.343 μs | 1.0487 μs | 0.9810 μs |      baseline |         |      40 B |             |
