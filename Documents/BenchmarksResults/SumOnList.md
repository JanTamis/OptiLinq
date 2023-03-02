## SumOnList

### Source
[SumOnList.cs](../../src/OptiLinq.Benchmark/SumOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------ |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|              Linq |  3.331 μs | 0.0241 μs | 0.0202 μs |     baseline |         |         - |          NA |
| OptiLinqOptimized |  9.357 μs | 0.0854 μs | 0.0757 μs | 2.81x slower |   0.03x |         - |          NA |
|          OptiLinq | 11.741 μs | 0.1477 μs | 0.1310 μs | 3.52x slower |   0.05x |      24 B |          NA |
