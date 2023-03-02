## TakeOnArrayWhere

### Source
[TakeOnArrayWhere.cs](../../src/StructLinq.Benchmark/TakeOnArrayWhere.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |     Mean |    Error |   StdDev | Ratio | Allocated | Alloc Ratio |
|------------ |---------:|---------:|---------:|------:|----------:|------------:|
|        Linq | 59.28 μs | 0.342 μs | 0.303 μs |  1.00 |     104 B |        1.00 |
|     LinqSum | 60.86 μs | 0.391 μs | 0.346 μs |  1.03 |     104 B |        1.00 |
|  StructLinq | 25.07 μs | 0.355 μs | 0.315 μs |  0.42 |         - |        0.00 |
| OptiLinqSum | 24.75 μs | 0.082 μs | 0.064 μs |  0.42 |      40 B |        0.38 |
