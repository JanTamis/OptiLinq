## ArrayOfClassSum

### Source
[ArrayOfClassSum.cs](../../src/StructLinq.Benchmark/ArrayOfClassSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|       Method |        Mean |    Error |   StdDev | Ratio | Allocated | Alloc Ratio |
|------------- |------------:|---------:|---------:|------:|----------:|------------:|
|    Handmaded |    505.7 ns |  1.45 ns |  1.21 ns |  0.07 |         - |        0.00 |
|      LINQSum |  7,430.6 ns | 14.87 ns | 12.42 ns |  1.00 |      48 B |        1.00 |
|  DelegateSum | 11,332.7 ns | 30.63 ns | 25.58 ns |  1.53 |      32 B |        0.67 |
| IFunctionSum |  3,713.8 ns |  3.02 ns |  2.36 ns |  0.50 |      24 B |        0.50 |
