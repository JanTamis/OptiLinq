## Concat

### Source
[Concat.cs](../../src/OptiLinq.Benchmark/Concat.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |      Mean |     Error |    StdDev | Allocated |
|------------ |----------:|----------:|----------:|----------:|
| OptiLinqSum |  5.121 μs | 0.0342 μs | 0.0304 μs |      32 B |
|    OptiLinq | 42.790 μs | 0.4560 μs | 0.3808 μs |      32 B |
|        Linq | 85.919 μs | 1.0458 μs | 0.9782 μs |     120 B |
|     LinqSum | 86.865 μs | 0.5529 μs | 0.4617 μs |     120 B |
