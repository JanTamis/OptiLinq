## Reverse

### Source
[Reverse.cs](../../src/OptiLinq.Benchmark/Reverse.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev |    Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|---------:|--------:|-------:|----------:|------------:|
| OptiLinq |       NA |       NA |       NA |        ? |       ? |      - |         - |           ? |
|     Linq | 46.31 μs | 0.777 μs | 0.727 μs | baseline |         | 4.7607 |   40072 B |             |

Benchmarks with issues:
  Reverse.OptiLinq: DefaultJob
