## ArrayOfBigStructSum

### Source
[ArrayOfBigStructSum.cs](../../src/StructLinq.Benchmark/ArrayOfBigStructSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |       Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|----------------- |-----------:|---------:|---------:|------:|-------:|----------:|------------:|
|        Handmaded |   580.2 ns |  7.90 ns |  7.00 ns |  0.06 |      - |         - |        0.00 |
| SysEnumerableSum | 8,932.9 ns | 84.62 ns | 79.15 ns |  1.00 |      - |      32 B |        1.00 |
|      DelegateSum | 2,387.8 ns | 35.25 ns | 32.98 ns |  0.27 |      - |      24 B |        0.75 |
|     IFunctionSum |   771.9 ns | 15.04 ns | 16.72 ns |  0.09 | 0.0029 |      24 B |        0.75 |
