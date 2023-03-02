## TakeWhileOnArray

### Source
[TakeWhileOnArray.cs](../../src/OptiLinq.Benchmark/TakeWhileOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |      Mean |     Error |    StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|-------------------- |----------:|----------:|----------:|-------------:|--------:|-------:|----------:|------------:|
|            OptiLinq |  9.268 ns | 0.1647 ns | 0.1460 ns | 4.18x faster |   0.07x |      - |         - |          NA |
|    OptiLinqFunction | 12.842 ns | 0.0995 ns | 0.0882 ns | 3.02x faster |   0.04x |      - |         - |          NA |
| OptiLinqFunctionSum | 18.967 ns | 0.3408 ns | 0.2846 ns | 2.04x faster |   0.04x | 0.0048 |      40 B |  2.60x less |
|         OptiLinqSum | 23.201 ns | 0.4677 ns | 0.4375 ns | 1.67x faster |   0.03x | 0.0048 |      40 B |  2.60x less |
|                Linq | 38.753 ns | 0.5592 ns | 0.4957 ns |     baseline |         | 0.0124 |     104 B |             |
|             LinqSum | 72.960 ns | 1.4194 ns | 1.2583 ns | 1.88x slower |   0.03x | 0.0124 |     104 B |  1.00x more |
