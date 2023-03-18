## Max

### Source
[Max.cs](../../src/OptiLinq.Benchmark/Max.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |        Mean |     Error |    StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |------------:|----------:|----------:|--------------:|--------:|-------:|----------:|------------:|
| OptiLinqOptimized |    534.5 ns |   3.89 ns |   3.45 ns | 11.42x faster |   0.15x | 0.0029 |      24 B |          NA |
|              LINQ |  1,078.0 ns |   1.14 ns |   1.01 ns |  5.66x faster |   0.05x |      - |         - |          NA |
|         Handmaded |  6,103.2 ns |  66.25 ns |  58.73 ns |      baseline |         |      - |         - |          NA |
|          OptiLinq | 18,344.6 ns | 129.68 ns | 121.30 ns |  3.00x slower |   0.03x |      - |         - |          NA |
