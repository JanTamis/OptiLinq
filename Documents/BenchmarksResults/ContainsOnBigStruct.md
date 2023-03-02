## ContainsOnBigStruct

### Source
[ContainsOnBigStruct.cs](../../src/OptiLinq.Benchmark/ContainsOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                     Method |         Mean |      Error |     StdDev |           Ratio | RatioSD |     Gen0 | Allocated | Alloc Ratio |
|--------------------------- |-------------:|-----------:|-----------:|----------------:|--------:|---------:|----------:|------------:|
| OptiLinqWithCustomComparer |     6.296 μs |  0.0458 μs |  0.0406 μs | 218.905x faster |   4.18x |        - |         - |          NA |
|                   OptiLinq | 1,270.410 μs | 12.2474 μs | 10.8570 μs |   1.085x faster |   0.02x | 152.3438 | 1280650 B |  1.00x more |
|                      Array | 1,373.311 μs | 13.1138 μs | 12.2666 μs |   1.002x faster |   0.02x | 152.3438 | 1280650 B |  1.00x more |
|                       Linq | 1,375.833 μs | 23.6665 μs | 22.1377 μs |        baseline |         | 152.3438 | 1280650 B |             |
