## AnyOnArray

### Source
[AnyOnArray.cs](../../src/OptiLinq.Benchmark/AnyOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|--------------:|--------:|-------:|----------:|------------:|
|               For |   261.1 ns |  4.18 ns |  4.10 ns | 11.14x faster |   0.21x |      - |         - |          NA |
| IFunctionOptiLinq |   263.4 ns |  4.95 ns |  4.39 ns | 11.04x faster |   0.20x |      - |         - |          NA |
|  DelegateOptiLinq | 1,025.8 ns | 10.42 ns |  8.70 ns |  2.84x faster |   0.03x | 0.0019 |      24 B |  1.33x less |
|              Linq | 2,912.9 ns | 16.59 ns | 12.95 ns |      baseline |         | 0.0038 |      32 B |             |
