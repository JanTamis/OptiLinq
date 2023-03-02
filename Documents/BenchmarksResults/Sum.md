## Sum

### Source
[Sum.cs](../../src/StructLinq.Benchmark/Sum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |           Mean |       Error |      StdDev |         Median |               Ratio |    RatioSD | Allocated | Alloc Ratio |
|--------------- |---------------:|------------:|------------:|---------------:|--------------------:|-----------:|----------:|------------:|
|        OptiSum |      0.0216 ns |   0.0174 ns |   0.0154 ns |      0.0144 ns | 203,482.648x faster | 96,804.69x |         - |          NA |
| OptiForEachSum |  2,829.7165 ns |  25.2856 ns |  23.6522 ns |  2,828.4481 ns |       1.098x faster |      0.05x |         - |          NA |
|         ForSum |  2,897.6199 ns |  57.6430 ns | 138.1089 ns |  2,822.2661 ns |            baseline |            |         - |          NA |
|     ConvertSum | 42,167.8655 ns | 176.3082 ns | 137.6498 ns | 42,208.2727 ns |      13.423x slower |      0.50x |      64 B |          NA |
|         SysSum | 46,286.9419 ns | 643.6034 ns | 602.0270 ns | 46,022.4891 ns |      14.928x slower |      0.73x |      40 B |          NA |
