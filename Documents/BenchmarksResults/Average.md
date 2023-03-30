## Average

### Source
[Average.cs](../../src/OptiLinq.Benchmark/Average.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |          Mean |       Error |      StdDev |           Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------- |--------------:|------------:|------------:|----------------:|--------:|-------:|----------:|------------:|
|        OptiAverage |      7.809 ns |   0.1717 ns |   0.1522 ns | 327.138x faster |   6.49x | 0.0105 |      88 B |          NA |
|         ForAverage |  2,551.200 ns |  22.1722 ns |  18.5148 ns |        baseline |         |      - |         - |          NA |
| OptiForEachAverage |  4,170.316 ns |  33.5734 ns |  29.7620 ns |   1.634x slower |   0.01x | 0.0076 |      88 B |          NA |
|     ConvertAverage | 36,227.486 ns | 580.6302 ns | 514.7136 ns |  14.202x slower |   0.26x |      - |      64 B |          NA |
|         SysAverage | 40,957.817 ns | 268.7659 ns | 224.4317 ns |  16.055x slower |   0.13x |      - |      40 B |          NA |
