## Sum

### Source
[Sum.cs](../../src/OptiLinq.Benchmark/Sum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |           Mean |       Error |      StdDev |         Median |               Ratio |       RatioSD | Allocated | Alloc Ratio |
|--------------- |---------------:|------------:|------------:|---------------:|--------------------:|--------------:|----------:|------------:|
|        OptiSum |      0.0140 ns |   0.0111 ns |   0.0099 ns |      0.0108 ns | 994,969.616x faster | 2,817,929.25x |         - |          NA |
| OptiForEachSum |  2,601.2183 ns |  18.5346 ns |  17.3373 ns |  2,598.1095 ns |       1.011x faster |         0.01x |         - |          NA |
|         ForSum |  2,628.5953 ns |  33.7402 ns |  31.5606 ns |  2,616.3910 ns |            baseline |               |         - |          NA |
|     ConvertSum | 39,246.5729 ns | 347.7292 ns | 290.3697 ns | 39,215.9692 ns |      14.928x slower |         0.23x |      64 B |          NA |
|         SysSum | 42,709.8921 ns | 294.6243 ns | 275.5918 ns | 42,696.3519 ns |      16.250x slower |         0.23x |      40 B |          NA |
