## SelectOnArrayOfClass

### Source
[SelectOnArrayOfClass.cs](../../src/OptiLinq.Benchmark/SelectOnArrayOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|            Handmaded |  5.630 μs | 0.0824 μs | 0.0771 μs |     baseline |         |         - |          NA |
| OptiLINQWithFunction | 17.251 μs | 0.1368 μs | 0.1068 μs | 3.08x slower |   0.05x |         - |          NA |
|             OptiLINQ | 23.648 μs | 0.2137 μs | 0.1894 μs | 4.20x slower |   0.07x |         - |          NA |
|                 LINQ | 52.529 μs | 0.5112 μs | 0.4782 μs | 9.33x slower |   0.13x |      48 B |          NA |
