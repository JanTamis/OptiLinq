## Average

### Source
[Average.cs](../../src/OptiLinq.Benchmark/Average.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |           Mean |       Error |      StdDev |         Median |          Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------- |---------------:|------------:|------------:|---------------:|---------------:|--------:|----------:|------------:|
|        OptiAverage |      0.0026 ns |   0.0049 ns |   0.0043 ns |      0.0005 ns |             NA |      NA |         - |          NA |
|         ForAverage |  2,615.1529 ns |  19.7398 ns |  17.4988 ns |  2,615.6247 ns |       baseline |         |         - |          NA |
| OptiForEachAverage |  4,262.7648 ns |  55.2277 ns |  89.1824 ns |  4,245.4623 ns |  1.643x slower |   0.05x |         - |          NA |
|         SysAverage | 42,097.5231 ns | 338.9161 ns | 300.4403 ns | 42,015.9490 ns | 16.098x slower |   0.17x |      40 B |          NA |
|     ConvertAverage | 53,294.7176 ns | 962.2725 ns | 853.0295 ns | 53,205.7473 ns | 20.380x slower |   0.36x |      64 B |          NA |
