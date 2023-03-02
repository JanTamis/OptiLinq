## AllOnArray

### Source
[AllOnArray.cs](../../src/StructLinq.Benchmark/AllOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|------:|-------:|----------:|------------:|
|               For |   190.1 ns |  2.36 ns |  2.21 ns |  0.06 |      - |         - |        0.00 |
|              Linq | 3,255.8 ns | 35.81 ns | 31.74 ns |  1.00 | 0.0038 |      32 B |        1.00 |
|  DelegateOptiLinq | 1,171.7 ns | 13.77 ns | 15.85 ns |  0.36 | 0.0019 |      24 B |        0.75 |
| IFunctionOptiLinq |   310.8 ns |  6.19 ns | 14.83 ns |  0.10 |      - |         - |        0.00 |
