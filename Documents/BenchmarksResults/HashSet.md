## HashSet

### Source
[HashSet.cs](../../src/OptiLinq.Benchmark/HashSet.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method | ItemCount |         Mean |       Error |      StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |---------- |-------------:|------------:|------------:|-------------:|--------:|-------:|----------:|------------:|
|        LINQ |         2 |     7.847 ns |   0.0670 ns |   0.0627 ns |     baseline |         |      - |         - |          NA |
|    OptiLINQ |         2 |    30.502 ns |   0.3222 ns |   0.2857 ns | 3.89x slower |   0.04x | 0.0048 |      40 B |          NA |
|     LinqSum |         2 |    31.911 ns |   0.3624 ns |   0.3390 ns | 4.07x slower |   0.05x | 0.0048 |      40 B |          NA |
| OptiLINQSum |         2 |    47.299 ns |   0.6099 ns |   0.5407 ns | 6.03x slower |   0.09x | 0.0076 |      64 B |          NA |
|             |           |              |             |             |              |         |        |           |             |
|        LINQ |       100 |   268.795 ns |   3.0223 ns |   2.6792 ns |     baseline |         |      - |         - |          NA |
| OptiLINQSum |       100 |   748.168 ns |   5.8919 ns |   4.9200 ns | 2.78x slower |   0.03x | 0.0076 |      64 B |          NA |
|    OptiLINQ |       100 |   756.218 ns |   6.1175 ns |   5.4230 ns | 2.81x slower |   0.04x | 0.0048 |      40 B |          NA |
|     LinqSum |       100 |   800.794 ns |   7.7969 ns |   6.9118 ns | 2.98x slower |   0.04x | 0.0048 |      40 B |          NA |
|             |           |              |             |             |              |         |        |           |             |
|        LINQ |      1000 | 2,761.487 ns |  54.1218 ns |  47.9776 ns |     baseline |         |      - |         - |          NA |
|    OptiLINQ |      1000 | 7,065.783 ns |  40.1914 ns |  35.6287 ns | 2.56x slower |   0.05x |      - |      40 B |          NA |
|     LinqSum |      1000 | 7,334.317 ns |  39.6327 ns |  35.1334 ns | 2.66x slower |   0.04x |      - |      40 B |          NA |
| OptiLINQSum |      1000 | 7,884.208 ns | 135.4368 ns | 126.6877 ns | 2.86x slower |   0.06x | 0.0076 |      64 B |          NA |
