## Contains

### Source
[Contains.cs](../../src/OptiLinq.Benchmark/Contains.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |   Error |  StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|--------:|--------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 380.9 ns | 4.32 ns | 4.04 ns | 1.03x faster |   0.01x |         - |          NA |
|    Array | 391.9 ns | 2.42 ns | 2.15 ns |     baseline |         |         - |          NA |
|     Linq | 393.6 ns | 5.77 ns | 5.12 ns | 1.00x slower |   0.01x |         - |          NA |
