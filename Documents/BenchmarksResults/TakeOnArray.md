## TakeOnArray

### Source
[TakeOnArray.cs](../../src/OptiLinq.Benchmark/TakeOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |        Mean |     Error |    StdDev |           Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |------------:|----------:|----------:|----------------:|--------:|-------:|----------:|------------:|
| OptiLinqSum |    240.4 ns |   3.23 ns |   2.70 ns | 151.885x faster |   2.90x | 0.0038 |      32 B |  1.50x less |
|    OptiLinq |  6,332.1 ns |  51.08 ns |  39.88 ns |   5.761x faster |   0.08x |      - |         - |          NA |
|        Linq | 36,456.8 ns | 510.05 ns | 452.14 ns |        baseline |         |      - |      48 B |             |
|     LinqSum | 38,967.2 ns | 579.28 ns | 483.72 ns |   1.068x slower |   0.02x |      - |      48 B |  1.00x more |
