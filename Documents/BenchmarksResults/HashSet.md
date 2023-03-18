## HashSet

### Source
[HashSet.cs](../../src/OptiLinq.Benchmark/HashSet.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method | ItemCount |         Mean |      Error |      StdDev |       Median |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |---------- |-------------:|-----------:|------------:|-------------:|-------------:|--------:|-------:|----------:|------------:|
|    OptiLINQ |         2 |     8.006 ns |  0.1949 ns |   0.4154 ns |     7.893 ns | 1.01x faster |   0.07x |      - |         - |          NA |
|        LINQ |         2 |     8.380 ns |  0.1845 ns |   0.1726 ns |     8.330 ns |     baseline |         |      - |         - |          NA |
| OptiLINQSum |         2 |    22.051 ns |  0.4443 ns |   0.4562 ns |    21.978 ns | 2.63x slower |   0.09x | 0.0029 |      24 B |          NA |
|     LinqSum |         2 |    34.532 ns |  0.4007 ns |   0.3552 ns |    34.462 ns | 4.12x slower |   0.08x | 0.0048 |      40 B |          NA |
|             |           |              |            |             |              |              |         |        |           |             |
|    OptiLINQ |       100 |   270.302 ns |  4.0873 ns |   3.6233 ns |   269.891 ns | 1.00x faster |   0.01x |      - |         - |          NA |
|        LINQ |       100 |   270.361 ns |  2.9691 ns |   2.6320 ns |   270.346 ns |     baseline |         |      - |         - |          NA |
| OptiLINQSum |       100 |   288.969 ns |  3.0201 ns |   2.6773 ns |   289.582 ns | 1.07x slower |   0.01x | 0.0029 |      24 B |          NA |
|     LinqSum |       100 |   800.770 ns | 10.0307 ns |   8.3761 ns |   799.711 ns | 2.96x slower |   0.04x | 0.0048 |      40 B |          NA |
|             |           |              |            |             |              |              |         |        |           |             |
|    OptiLINQ |      1000 | 2,604.579 ns | 29.9201 ns |  26.5234 ns | 2,596.790 ns | 1.01x faster |   0.02x |      - |         - |          NA |
|        LINQ |      1000 | 2,628.782 ns | 43.6996 ns |  38.7386 ns | 2,612.967 ns |     baseline |         |      - |         - |          NA |
| OptiLINQSum |      1000 | 2,850.298 ns | 56.9657 ns | 158.7975 ns | 2,783.450 ns | 1.17x slower |   0.05x |      - |      24 B |          NA |
|     LinqSum |      1000 | 7,384.907 ns | 75.7366 ns |  67.1385 ns | 7,355.687 ns | 2.81x slower |   0.05x |      - |      40 B |          NA |
