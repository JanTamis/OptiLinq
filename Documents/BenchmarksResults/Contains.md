## Contains

### Source
[Contains.cs](../../src/StructLinq.Benchmark/Contains.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |   Error |  StdDev | Ratio | Allocated | Alloc Ratio |
|--------- |---------:|--------:|--------:|------:|----------:|------------:|
|    Array | 427.2 ns | 1.70 ns | 1.51 ns |  1.00 |         - |          NA |
|     Linq | 426.0 ns | 0.98 ns | 0.77 ns |  1.00 |         - |          NA |
| OptiLinq | 427.6 ns | 0.37 ns | 0.31 ns |  1.00 |         - |          NA |
