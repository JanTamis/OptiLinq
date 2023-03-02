## SumOnList

### Source
[SumOnList.cs](../../src/StructLinq.Benchmark/SumOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |      Mean |     Error |    StdDev |    Median |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------ |----------:|----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|              Linq |  5.155 μs | 0.2041 μs | 0.5986 μs |  5.012 μs |     baseline |         |         - |          NA |
| OptiLinqOptimized | 14.693 μs | 0.5724 μs | 1.6878 μs | 14.217 μs | 2.90x slower |   0.51x |         - |          NA |
|          OptiLinq | 15.985 μs | 0.4746 μs | 1.3918 μs | 15.865 μs | 3.13x slower |   0.40x |      24 B |          NA |
