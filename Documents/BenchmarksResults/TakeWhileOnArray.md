## TakeWhileOnArray

### Source
[TakeWhileOnArray.cs](../../src/StructLinq.Benchmark/TakeWhileOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |     Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|-------------------- |---------:|---------:|---------:|-------------:|--------:|-------:|----------:|------------:|
|            OptiLinq | 11.57 ns | 0.267 ns | 0.223 ns | 4.37x faster |   0.07x |      - |         - |          NA |
|    OptiLinqFunction | 15.20 ns | 0.018 ns | 0.014 ns | 3.32x faster |   0.06x |      - |         - |          NA |
| OptiLinqFunctionSum | 23.50 ns | 0.062 ns | 0.058 ns | 2.15x faster |   0.03x | 0.0048 |      40 B |  2.60x less |
|         OptiLinqSum | 28.98 ns | 0.536 ns | 0.475 ns | 1.74x faster |   0.02x | 0.0048 |      40 B |  2.60x less |
|                Linq | 50.48 ns | 0.880 ns | 0.781 ns |     baseline |         | 0.0124 |     104 B |             |
|             LinqSum | 61.14 ns | 2.769 ns | 8.165 ns | 1.17x slower |   0.17x | 0.0124 |     104 B |  1.00x more |
