## Aggregate

### Source
[Aggregate.cs](../../src/OptiLinq.Benchmark/Aggregate.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
| IFunctionAggregate |  2.714 μs | 0.0681 μs | 0.1999 μs | 19.04x faster |   1.44x |         - |          NA |
|  DelegateAggregate | 25.010 μs | 0.4991 μs | 1.2612 μs |  2.26x faster |   0.08x |      24 B |  1.67x less |
|   ConvertAggregate | 39.749 μs | 0.5315 μs | 0.4712 μs |  1.35x faster |   0.02x |      40 B |  1.00x more |
|       SysAggregate | 53.727 μs | 0.6339 μs | 0.4949 μs |      baseline |         |      40 B |             |
