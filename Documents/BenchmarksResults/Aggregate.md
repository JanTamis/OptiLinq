## Aggregate

### Source
[Aggregate.cs](../../src/StructLinq.Benchmark/Aggregate.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------ |----------:|----------:|----------:|----------:|------:|--------:|----------:|------------:|
|      SysAggregate | 57.344 μs | 1.1353 μs | 1.2619 μs | 56.386 μs |  1.00 |    0.00 |      40 B |        1.00 |
| DelegateAggregate | 25.314 μs | 0.0272 μs | 0.0227 μs | 25.309 μs |  0.44 |    0.01 |      24 B |        0.60 |
|   StructAggregate |  2.828 μs | 0.0152 μs | 0.0142 μs |  2.828 μs |  0.05 |    0.00 |         - |        0.00 |
|  ConvertAggregate | 42.863 μs | 0.7382 μs | 0.7250 μs | 42.655 μs |  0.75 |    0.03 |      40 B |        1.00 |
