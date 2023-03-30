## AverageOnList

### Source
[AverageOnList.cs](../../src/OptiLinq.Benchmark/AverageOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |       Mean |    Error |   StdDev |     Median |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------- |-----------:|---------:|---------:|-----------:|-------------:|--------:|-------:|----------:|------------:|
| OptiLinq |   369.9 ns |  2.74 ns |  2.43 ns |   369.8 ns | 6.45x faster |   0.24x | 0.0029 |      24 B |          NA |
|     Linq | 2,328.0 ns | 45.69 ns | 81.21 ns | 2,290.7 ns |     baseline |         |      - |         - |          NA |
