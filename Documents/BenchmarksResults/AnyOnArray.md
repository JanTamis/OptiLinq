## AnyOnArray

### Source
[AnyOnArray.cs](../../src/StructLinq.Benchmark/AnyOnArray.cs)

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
|               For |   279.6 ns |  4.10 ns |  3.84 ns |  0.09 |      - |         - |        0.00 |
|              Linq | 3,219.1 ns | 43.69 ns | 40.87 ns |  1.00 | 0.0038 |      32 B |        1.00 |
|  DelegateOptiLinq | 1,113.3 ns | 17.75 ns | 16.60 ns |  0.35 | 0.0019 |      24 B |        0.75 |
| IFunctionOptiLinq |   279.0 ns |  3.13 ns |  2.77 ns |  0.09 |      - |         - |        0.00 |
