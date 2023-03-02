## Max

### Source
[Max.cs](../../src/StructLinq.Benchmark/Max.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|     Method |           Mean |       Error |        StdDev |         Median |               Ratio |     RatioSD | Allocated | Alloc Ratio |
|----------- |---------------:|------------:|--------------:|---------------:|--------------------:|------------:|----------:|------------:|
| StructLINQ |      0.0250 ns |   0.0232 ns |     0.0194 ns |      0.0142 ns | 222,447.832x faster | 163,944.06x |         - |          NA |
|  Handmaded |  2,890.1148 ns |  51.1778 ns |    71.7441 ns |  2,878.9305 ns |            baseline |             |         - |          NA |
| ConvertMin | 42,677.9239 ns | 632.5419 ns |   528.2012 ns | 42,377.7101 ns |      14.665x slower |       0.49x |      40 B |          NA |
|       LINQ | 43,200.4359 ns | 842.1031 ns | 1,034.1782 ns | 42,581.4508 ns |      14.965x slower |       0.48x |      40 B |          NA |
