## ArraySelectCount

### Source
[ArraySelectCount.cs](../../src/StructLinq.Benchmark/ArraySelectCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|          Method |           Mean |       Error |      StdDev | Ratio | Allocated | Alloc Ratio |
|---------------- |---------------:|------------:|------------:|------:|----------:|------------:|
|            Linq | 20,652.5252 ns | 325.7875 ns | 288.8021 ns | 1.000 |      48 B |        1.00 |
|  DelegateSelect |      0.7355 ns |   0.0132 ns |   0.0117 ns | 0.000 |         - |        0.00 |
| IFunctionSelect |      0.0308 ns |   0.0139 ns |   0.0123 ns | 0.000 |         - |        0.00 |
