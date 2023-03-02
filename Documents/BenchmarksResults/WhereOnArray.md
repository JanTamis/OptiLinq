## WhereOnArray

### Source
[WhereOnArray.cs](../../src/OptiLinq.Benchmark/WhereOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                  Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|               Handmaded |  6.929 μs | 0.1356 μs | 0.1059 μs |     baseline |         |         - |          NA |
| OptiLinqWithFunctionSum | 11.235 μs | 0.1393 μs | 0.1303 μs | 1.62x slower |   0.03x |      32 B |          NA |
|    OptiLinqWithFunction | 22.069 μs | 0.1647 μs | 0.1460 μs | 3.18x slower |   0.05x |         - |          NA |
|             OptiLinqSum | 25.260 μs | 0.1941 μs | 0.1721 μs | 3.65x slower |   0.06x |      32 B |          NA |
|                OptiLinq | 32.699 μs | 0.4265 μs | 0.3989 μs | 4.72x slower |   0.11x |         - |          NA |
|                 LinqSum | 43.913 μs | 0.6357 μs | 0.5635 μs | 6.33x slower |   0.11x |      48 B |          NA |
|                    LINQ | 44.812 μs | 1.0832 μs | 3.1937 μs | 6.86x slower |   0.44x |      48 B |          NA |
