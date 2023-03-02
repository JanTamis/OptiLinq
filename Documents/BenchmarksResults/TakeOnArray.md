## TakeOnArray

### Source
[TakeOnArray.cs](../../src/StructLinq.Benchmark/TakeOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |        Mean |     Error |    StdDev |           Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |------------:|----------:|----------:|----------------:|--------:|-------:|----------:|------------:|
| OptiLinqSum |    297.7 ns |   1.52 ns |   1.35 ns | 152.073x faster |   1.14x | 0.0038 |      32 B |  1.50x less |
|    OptiLinq | 13,016.2 ns |  79.96 ns |  70.88 ns |   3.473x faster |   0.03x |      - |         - |          NA |
|        Linq | 45,262.6 ns | 288.79 ns | 225.47 ns |        baseline |         |      - |      48 B |             |
|     LinqSum | 48,069.7 ns | 211.81 ns | 176.87 ns |   1.062x slower |   0.00x |      - |      48 B |  1.00x more |
