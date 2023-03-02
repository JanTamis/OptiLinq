## HashSet

### Source
[HashSet.cs](../../src/StructLinq.Benchmark/HashSet.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method | ItemCount |         Mean |      Error |     StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |---------- |-------------:|-----------:|-----------:|-------------:|--------:|-------:|----------:|------------:|
|        LINQ |         2 |     8.985 ns |  0.2105 ns |  0.1969 ns |     baseline |         |      - |         - |          NA |
|    OptiLINQ |         2 |    37.248 ns |  0.1380 ns |  0.1223 ns | 4.15x slower |   0.10x | 0.0048 |      40 B |          NA |
|     LinqSum |         2 |    38.178 ns |  0.0872 ns |  0.0773 ns | 4.25x slower |   0.09x | 0.0048 |      40 B |          NA |
| OptiLINQSum |         2 |    48.019 ns |  0.8347 ns |  0.7808 ns | 5.35x slower |   0.18x | 0.0076 |      64 B |          NA |
|             |           |              |            |            |              |         |        |           |             |
|        LINQ |       100 |   308.717 ns |  0.5208 ns |  0.4617 ns |     baseline |         |      - |         - |          NA |
|    OptiLINQ |       100 |   826.612 ns | 16.0060 ns | 14.9720 ns | 2.67x slower |   0.05x | 0.0048 |      40 B |          NA |
| OptiLINQSum |       100 |   852.869 ns |  1.6290 ns |  1.2718 ns | 2.76x slower |   0.01x | 0.0076 |      64 B |          NA |
|     LinqSum |       100 |   890.121 ns |  1.6813 ns |  1.4040 ns | 2.88x slower |   0.01x | 0.0048 |      40 B |          NA |
|             |           |              |            |            |              |         |        |           |             |
|        LINQ |      1000 | 2,981.798 ns |  4.7778 ns |  3.7302 ns |     baseline |         |      - |         - |          NA |
|    OptiLINQ |      1000 | 8,163.051 ns | 24.4744 ns | 20.4372 ns | 2.74x slower |   0.01x |      - |      40 B |          NA |
| OptiLINQSum |      1000 | 8,168.857 ns | 18.6970 ns | 16.5744 ns | 2.74x slower |   0.01x |      - |      64 B |          NA |
|     LinqSum |      1000 | 8,451.649 ns | 16.0580 ns | 14.2350 ns | 2.83x slower |   0.00x |      - |      40 B |          NA |
