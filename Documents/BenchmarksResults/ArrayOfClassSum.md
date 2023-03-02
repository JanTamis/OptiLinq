## ArrayOfClassSum

### Source
[ArrayOfClassSum.cs](../../src/OptiLinq.Benchmark/ArrayOfClassSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|       Method |        Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------- |------------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|    Handmaded |    452.1 ns |   4.99 ns |   4.67 ns | 14.68x faster |   0.18x |         - |          NA |
| IFunctionSum |  3,295.4 ns |  17.28 ns |  14.43 ns |  2.01x faster |   0.02x |      24 B |  2.00x less |
|      LINQSum |  6,635.7 ns |  77.12 ns |  64.40 ns |      baseline |         |      48 B |             |
|  DelegateSum | 10,866.1 ns | 184.64 ns | 281.97 ns |  1.65x slower |   0.05x |      32 B |  1.50x less |
