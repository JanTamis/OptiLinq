## ContainsOnBigStruct

### Source
[ContainsOnBigStruct.cs](../../src/StructLinq.Benchmark/ContainsOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                     Method |         Mean |      Error |     StdDev | Ratio | RatioSD |     Gen0 | Allocated | Alloc Ratio |
|--------------------------- |-------------:|-----------:|-----------:|------:|--------:|---------:|----------:|------------:|
|                       Linq | 1,560.013 μs | 22.8059 μs | 21.3326 μs | 1.000 |    0.00 | 152.3438 | 1280650 B |        1.00 |
|                      Array | 1,512.675 μs |  5.2832 μs |  4.9419 μs | 0.970 |    0.01 | 152.3438 | 1280650 B |        1.00 |
|                   OptiLinq | 1,477.412 μs | 25.2317 μs | 23.6017 μs | 0.947 |    0.02 | 152.3438 | 1280650 B |        1.00 |
| OptiLinqWithCustomComparer |     7.296 μs |  0.0098 μs |  0.0081 μs | 0.005 |    0.00 |        - |         - |        0.00 |
