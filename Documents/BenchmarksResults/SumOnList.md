## SumOnList

### Source
[SumOnList.cs](../../src/OptiLinq.Benchmark/SumOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------ |-----------:|---------:|---------:|-------------:|--------:|-------:|----------:|------------:|
| OptiLinqOptimized |   371.1 ns |  2.90 ns |  2.57 ns | 9.94x faster |   0.08x |      - |         - |          NA |
|          OptiLinq |   380.1 ns |  3.72 ns |  3.48 ns | 9.68x faster |   0.09x | 0.0029 |      24 B |          NA |
|              Linq | 3,689.6 ns | 25.08 ns | 19.58 ns |     baseline |         |      - |         - |          NA |
