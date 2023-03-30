## ArrayOfIntSum

### Source
[ArrayOfIntSum.cs](../../src/OptiLinq.Benchmark/ArrayOfIntSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|-------------:|--------:|-------:|----------:|------------:|
| OptiLinqOptimized |   373.3 ns |  4.18 ns |  3.49 ns | 9.83x faster |   0.11x |      - |         - |          NA |
|          OptiLinq |   380.9 ns |  7.23 ns | 11.68 ns | 9.36x faster |   0.27x | 0.0029 |      24 B |          NA |
|         ArrayLINQ | 3,667.8 ns | 21.92 ns | 18.30 ns |     baseline |         |      - |         - |          NA |
|    EnumerableLINQ | 3,693.0 ns | 21.58 ns | 18.02 ns | 1.01x slower |   0.01x |      - |         - |          NA |
|         Handmaded | 4,567.9 ns | 66.32 ns | 55.38 ns | 1.25x slower |   0.02x |      - |         - |          NA |
