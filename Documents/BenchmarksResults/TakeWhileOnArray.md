## TakeWhileOnArray

### Source
[TakeWhileOnArray.cs](../../src/OptiLinq.Benchmark/TakeWhileOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |      Mean |     Error |    StdDev |    Median |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|-------------------- |----------:|----------:|----------:|----------:|-------------:|--------:|-------:|----------:|------------:|
|         OptiLinqSum |  8.115 ns | 0.1289 ns | 0.2453 ns |  8.062 ns | 5.45x faster |   0.16x |      - |         - |          NA |
|            OptiLinq |  8.213 ns | 0.0930 ns | 0.0824 ns |  8.227 ns | 5.38x faster |   0.09x |      - |         - |          NA |
| OptiLinqFunctionSum |  9.219 ns | 0.0806 ns | 0.0753 ns |  9.180 ns | 4.79x faster |   0.08x |      - |         - |          NA |
|    OptiLinqFunction | 13.186 ns | 0.0582 ns | 0.0544 ns | 13.171 ns | 3.35x faster |   0.05x |      - |         - |          NA |
|                Linq | 44.153 ns | 0.7762 ns | 0.6482 ns | 43.905 ns |     baseline |         | 0.0124 |     104 B |             |
|             LinqSum | 48.202 ns | 1.6363 ns | 4.6418 ns | 45.883 ns | 1.07x slower |   0.05x | 0.0124 |     104 B |  1.00x more |
