## AllOnArray

### Source
[AllOnArray.cs](../../src/OptiLinq.Benchmark/AllOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|--------------:|--------:|-------:|----------:|------------:|
|               For |   179.5 ns |  3.30 ns |  3.24 ns | 16.63x faster |   0.42x |      - |         - |          NA |
| IFunctionOptiLinq |   266.0 ns |  2.06 ns |  1.83 ns | 11.21x faster |   0.13x |      - |         - |          NA |
|  DelegateOptiLinq | 1,058.2 ns | 20.29 ns | 18.98 ns |  2.82x faster |   0.05x | 0.0019 |      24 B |  1.33x less |
|              Linq | 2,982.1 ns | 41.22 ns | 36.54 ns |      baseline |         | 0.0038 |      32 B |             |
