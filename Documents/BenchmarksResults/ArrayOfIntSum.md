## ArrayOfIntSum

### Source
[ArrayOfIntSum.cs](../../src/OptiLinq.Benchmark/ArrayOfIntSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |       Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |-----------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|       OptiLinq |   386.3 ns |  7.66 ns | 10.49 ns | 9.96x faster |   0.38x |         - |          NA |
| EnumerableLINQ | 3,835.4 ns | 75.24 ns | 89.57 ns | 1.00x faster |   0.03x |         - |          NA |
|      ArrayLINQ | 3,838.3 ns | 74.26 ns | 82.54 ns |     baseline |         |         - |          NA |
|      Handmaded | 3,877.3 ns | 75.29 ns | 86.71 ns | 1.01x slower |   0.03x |         - |          NA |
