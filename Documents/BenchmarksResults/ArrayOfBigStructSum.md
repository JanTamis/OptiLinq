## ArrayOfBigStructSum

### Source
[ArrayOfBigStructSum.cs](../../src/OptiLinq.Benchmark/ArrayOfBigStructSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|           Method |       Mean |     Error |   StdDev |         Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|----------------- |-----------:|----------:|---------:|--------------:|--------:|-------:|----------:|------------:|
|        Handmaded |   545.3 ns |  10.09 ns |  8.42 ns | 14.77x faster |   0.19x |      - |         - |          NA |
|     IFunctionSum |   715.9 ns |  11.71 ns |  9.14 ns | 11.27x faster |   0.21x | 0.0029 |      24 B |  1.33x less |
|      DelegateSum | 2,201.6 ns |  26.96 ns | 23.90 ns |  3.65x faster |   0.04x |      - |      24 B |  1.33x less |
| SysEnumerableSum | 8,053.9 ns | 110.25 ns | 92.06 ns |      baseline |         |      - |      32 B |             |
