## Contains

### Source
[Contains.cs](../../src/OptiLinq.Benchmark/Contains.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |   Error |  StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|--------:|--------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 309.1 ns | 2.33 ns | 2.18 ns | 1.23x faster |   0.01x |         - |          NA |
|     Linq | 379.2 ns | 3.56 ns | 3.33 ns | 1.00x faster |   0.01x |         - |          NA |
|    Array | 380.0 ns | 2.77 ns | 2.46 ns |     baseline |         |         - |          NA |
