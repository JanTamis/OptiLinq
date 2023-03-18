## AverageOnList

### Source
[AverageOnList.cs](../../src/OptiLinq.Benchmark/AverageOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |       Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |-----------:|---------:|---------:|-------------:|--------:|-------:|----------:|------------:|
| OptiLinq |   388.7 ns |  7.33 ns |  6.85 ns | 9.98x faster |   0.19x | 0.0029 |      24 B |          NA |
|     Linq | 3,874.1 ns | 47.38 ns | 42.00 ns |     baseline |         |      - |         - |          NA |
