## ArrayOfBigStructSum

### Source
[ArrayOfBigStructSum.cs](../../src/OptiLinq.Benchmark/ArrayOfBigStructSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |       Mean |    Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|----------------- |-----------:|---------:|---------:|--------------:|--------:|-------:|----------:|------------:|
|     IFunctionSum |   505.7 ns |  4.75 ns |  4.21 ns | 16.06x faster |   0.16x | 0.0029 |      24 B |  1.33x less |
|        Handmaded |   533.6 ns |  6.95 ns |  5.80 ns | 15.22x faster |   0.20x |      - |         - |          NA |
|      DelegateSum | 2,050.4 ns | 14.37 ns | 11.22 ns |  3.96x faster |   0.05x |      - |      24 B |  1.33x less |
| SysEnumerableSum | 8,115.1 ns | 72.10 ns | 67.44 ns |      baseline |         |      - |      32 B |             |
