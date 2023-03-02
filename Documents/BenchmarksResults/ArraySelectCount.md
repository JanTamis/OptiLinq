## ArraySelectCount

### Source
[ArraySelectCount.cs](../../src/OptiLinq.Benchmark/ArraySelectCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|          Method |           Mean |       Error |      StdDev |              Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------- |---------------:|------------:|------------:|-------------------:|--------:|----------:|------------:|
| IFunctionSelect |      0.0179 ns |   0.0177 ns |   0.0148 ns |                 NA |      NA |         - |          NA |
|  DelegateSelect |      0.6325 ns |   0.0198 ns |   0.0166 ns | 28,077.367x faster | 663.97x |         - |          NA |
|            Linq | 17,750.6053 ns | 239.1388 ns | 199.6917 ns |           baseline |         |      48 B |             |
