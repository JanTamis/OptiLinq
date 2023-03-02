## Concat

### Source
[Concat.cs](../../src/StructLinq.Benchmark/Concat.cs)

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
|        Linq | 89.998 μs | 1.5223 μs | 1.4240 μs |     120 B |
|     LinqSum | 92.582 μs | 1.8488 μs | 1.8157 μs |     120 B |
|    OptiLinq | 93.197 μs | 1.8389 μs | 1.8884 μs |      64 B |
| OptiLinqSum |  5.239 μs | 0.0939 μs | 0.0878 μs |      32 B |
