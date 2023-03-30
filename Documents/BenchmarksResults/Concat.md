## Concat

### Source
[Concat.cs](../../src/OptiLinq.Benchmark/Concat.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |         Mean |        Error |       StdDev |             Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------ |-------------:|-------------:|-------------:|------------------:|--------:|-------:|----------:|------------:|
|    OptiLinq |     19.24 ns |     0.194 ns |     0.172 ns | 4,551.223x faster |  85.08x | 0.0038 |      32 B |  3.75x less |
| OptiLinqSum |  3,351.38 ns |    34.674 ns |    30.738 ns |    26.129x faster |   0.40x |      - |         - |          NA |
|        Linq | 87,638.56 ns | 1,332.900 ns | 1,246.796 ns |          baseline |         |      - |     120 B |             |
|     LinqSum | 87,651.22 ns | 1,039.808 ns |   921.762 ns |     1.001x slower |   0.02x |      - |     120 B |  1.00x more |
