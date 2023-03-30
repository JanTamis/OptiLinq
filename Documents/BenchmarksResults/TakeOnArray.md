## TakeOnArray

### Source
[TakeOnArray.cs](../../src/OptiLinq.Benchmark/TakeOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |          Mean |       Error |        StdDev |        Median |              Ratio | RatioSD | Allocated | Alloc Ratio |
|------------ |--------------:|------------:|--------------:|--------------:|-------------------:|--------:|----------:|------------:|
| OptiLinqSum |      2.750 ns |   0.0514 ns |     0.0455 ns |      2.737 ns | 15,998.823x faster | 700.88x |         - |          NA |
|    OptiLinq |      6.725 ns |   0.1184 ns |     0.1050 ns |      6.700 ns |  6,541.005x faster | 283.89x |         - |          NA |
|     LinqSum | 42,899.011 ns | 849.1003 ns | 1,615.5015 ns | 42,292.224 ns |      1.032x faster |   0.06x |      48 B |  1.00x more |
|        Linq | 43,348.340 ns | 860.8544 ns | 2,143.8262 ns | 42,500.189 ns |           baseline |         |      48 B |             |
