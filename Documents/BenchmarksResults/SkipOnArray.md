## SkipOnArray

### Source
[SkipOnArray.cs](../../src/OptiLinq.Benchmark/SkipOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |          Mean |         Error |        StdDev |             Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |--------------:|--------------:|--------------:|------------------:|--------:|-------:|----------:|------------:|
|    OptiLinq |      8.316 ns |     0.0852 ns |     0.0755 ns | 8,619.234x faster | 158.83x |      - |         - |          NA |
| OptiLinqSum |    326.380 ns |     3.6535 ns |     3.0508 ns |   219.174x faster |   4.15x | 0.0038 |      32 B |  1.50x less |
|        Linq | 71,732.436 ns | 1,255.0734 ns | 1,173.9964 ns |          baseline |         |      - |      48 B |             |
