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
|         Method |           Mean |       Error |      StdDev |         Median |          Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |---------------:|------------:|------------:|---------------:|---------------:|--------:|----------:|------------:|
|        OptiSum |      0.0130 ns |   0.0156 ns |   0.0131 ns |      0.0101 ns |             NA |      NA |         - |          NA |
| OptiForEachSum |  2,360.6026 ns |  24.7042 ns |  23.1083 ns |  2,361.6677 ns |  1.005x faster |   0.01x |         - |          NA |
|         ForSum |  2,375.2569 ns |  18.5058 ns |  16.4049 ns |  2,376.4904 ns |       baseline |         |         - |          NA |
|     ConvertSum | 35,204.5440 ns | 347.1563 ns | 324.7302 ns | 35,334.3473 ns | 14.838x slower |   0.17x |      64 B |          NA |
|         SysSum | 38,668.2166 ns | 363.4098 ns | 339.9338 ns | 38,693.3932 ns | 16.274x slower |   0.19x |      40 B |          NA |
