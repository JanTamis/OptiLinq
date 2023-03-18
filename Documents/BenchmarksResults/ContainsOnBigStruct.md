## ContainsOnBigStruct

### Source
[ContainsOnBigStruct.cs](../../src/OptiLinq.Benchmark/ContainsOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                     Method |         Mean |      Error |     StdDev |           Ratio | RatioSD |     Gen0 | Allocated | Alloc Ratio |
|--------------------------- |-------------:|-----------:|-----------:|----------------:|--------:|---------:|----------:|------------:|
| OptiLinqWithCustomComparer |     8.089 μs |  0.0889 μs |  0.0832 μs | 154.904x faster |   3.58x |        - |         - |          NA |
|                   OptiLinq | 1,183.058 μs | 11.0122 μs | 10.3008 μs |   1.059x faster |   0.02x | 152.3438 | 1280650 B |  1.00x more |
|                       Linq | 1,251.310 μs | 23.5909 μs | 25.2420 μs |        baseline |         | 152.3438 | 1280650 B |             |
|                      Array | 1,262.845 μs | 25.0373 μs | 31.6641 μs |   1.016x slower |   0.03x | 152.3438 | 1280650 B |  1.00x more |
